
(function ($, ej, undefined) {

    ej.widget("ejTreeView", "ej.TreeView", {
        _rootCSS: "e-treeview",
        // widget element will be automatically set in this
        element: null,

        // user defined model will be automatically set in this
        model: null,
        validTags: ["ul", "div"],
        _setFirst: false,

        // default model
        defaults: {
            showCheckbox: false,
            dragAndDrop: false,
            dropChild: true,
            dropSibling: true,
            dragAndDropAcrossControl: true,
            allowEdit: false,
            allowKeyboardNavigation: true,
            items: null, //id, text, spriteCssClass,expanded, childItems ,selected ,linkAttribute ,value, imageAttribute, htmlAttribute,imageUrl
            fields: {
                dataSource: null,
                query: null,
                tableName: null,
                child: null,
                id: "id",
                parentId: "parentId",
                text: "text",
                spriteCssClass: "spriteCssClass",
                expanded: "expanded",
                hasChild: "hasChild",
                selected: "selected",
                linkAttribute: "linkAttribute",
                value: "value",
                imageAttribute: "imageAttribute",
                htmlAttribute: "htmlAttribute",
                imageUrl: "imageUrl",
                isChecked: "isChecked"
            }, //null, //id, parentId, text, spriteCssClass,expanded, childItems, hasChild, selected, linkAttribute ,value, imageAttribute, htmlAttribute, imageUrl, isChecked, tableName
            autoCheckParentNode: false,
            loadOnDemand: false,
            cssClass: "",
            template: null,
            rtl: false,
            expandEvent: "dblclick",
            persist: false,
            enabled: true,
            expandedNodes: [],
            width: null,
            height: null,
            click: null,
            beforeExpand: null,
            expand: null,
            beforeCollapse: null,
            collapse: null,
            select: null,
            check: null,
            uncheck: null,
            inlineEditValidation: null,
            beforeEdit: null,
            keyPress: null,
            dragStart: null,
            drag: null,
            dragStop: null,
            dropped: null
        },

        dataTypes: {
            showCheckbox: "boolean",
            dragAndDrop: "boolean",
            dropChild: "boolean",
            dragAndDropAcrossControl: "boolean",
            allowEdit: "boolean",
            allowKeyboardNavigation: "boolean",
            autoCheckParentNode: "boolean",
            loadOnDemand: "boolean",
            rtl: "boolean",
            expandEvent: "string",
            persist: "boolean",
            items: "data",
            fields: "data",
            expandedNodes: "data"
        },
        observables: ["fields.dataSource"],

        _setModel: function (options) {
            /// <summary>This function is used to set the Treeview model</summary>
            if (!this.model.enabled && ej.isNullOrUndefined(options["enabled"])) return false;
            for (var key in options) {
                switch (key) {
                    case "cssClass": this._changeSkin(options[key]); break;
                    case "dragAndDropAcrossControl":
                        {
                            this.model.dragAndDropAcrossControl = options[key];
                            this._enableDargDrop();
                            break;
                        }
                    case "enabled": this._enabledAction(options[key]); break;
                    case "showCheckbox": {
                        if (options[key]) {
                            this.model.showCheckbox = options[key];
                            this._showCheckBox();
                        } else {
                            this.element.find('span.e-chkbox-wrap').next().remove();
                            this.element.find('span.e-chkbox-wrap').remove();
                        }
                        break;
                    }
                    case "expandedNodes": {
                        for (var i = 0; i < options[key].length; i++) {
                            this._expandNode(this._liList[options[key][i]]);
                        }
                        break;
                    }
                    case "expandEvent": {
                        this._on(this.element, options[key], this._expandEventHandler);
                        break;
                    }
                    case "allowEdit": {
                        if (options[key])
                            this._enableAllowEdit();
                        else {
                            this._disableAllowEdit();
                        }
                        break;
                    }
                    case "allowKeyboardNavigation": {
                        if (options[key])
                            this._on($(document), 'keydown', this._KeyPress);
                        else
                            $(document).unbind('keydown', this._KeyPress);
                        break;
                    }
                    case "dragAndDrop":
                        {
                            if (options[key])
                                this._enableDargDrop();
                            break;
                        }
                    case "rtl":
                        {
                            if (options[key])
                                this.element.addClass("e-rtl");
                            else
                                this.element.removeClass("e-rtl");
                            break;
                        }
                    case "height":
                        {
                            this.element.height(options[key]);
                            break;
                        }
                    case "width":
                        {
                            this.element.width(options[key]);
                            break;
                        }
                }
            }
        },

        // all events bound using this._on will be unbind automatically
        _destroy: function () {
            this.element.html("");
            this.element.parent().append(this._cloneElement);
            this.element.remove();
            this.element = this._cloneElement;
            this.element.removeClass("e-treeview e-widget");
        },

        // constructor function
        _init: function () {
          
            this._id = this.element.prop("id");
            if (this.model.items != null) {
                var main = this._renderTemplate(this.model.items);
                this.element.append(main);
            }
            if (!ej.isNullOrUndefined(this.model.fields) && this.model.fields["dataSource"] != null) {
                this._checkDataBinding();
            } else
                this.initialize();
	
        },

        initialize: function () {
            this._cloneElement = $(this.element).clone();
            this._cutCopyNodeText = null;
            this.beforeEditText = null;
            if (this.element.is("ul")) {
                this._createWrapper();
            }
            else {
                this.element.addClass("e-treeview-wrap e-widget");
                if (this.model.width != null) {
                    this.element.width(this.model.width);
                }
                if (this.model.height != null) {
                    this.element.height(this.model.height);
                }
                if (this.model.rtl)
                    this.element.addClass("e-rtl");
            }
            this.element.attr("role", "tree").attr("aria-activedescendant", this._id + "_active");
            this._ulList = $("ul", this.element);
            this._liList = $("li", this.element).attr("tabindex", -1);
            this._addBaseClass();
            this._controlRender();
            this._addDragableClass();
            this._finalize();
            this._enabledAction(this.model.enabled);
        },

        _createWrapper: function () {
            var mainWidget = ej.buildTag("div.e-treeview-wrap e-widget " + this.model.cssClass, "", "", { tabindex: 0 });
            !ej.isNullOrUndefined(this.element.attr("accesskey")) && mainWidget.attr("accesskey", this.element.attr("accesskey"));
            if (this.model.width != null) {
                mainWidget.width(this.model.width);
            }
            if (this.model.height != null) {
                mainWidget.height(this.model.height);
            }
            if (this.model.rtl)
                mainWidget.addClass("e-rtl");
            mainWidget.insertAfter(this.element);
            mainWidget.append(this.element.addClass("e-ul"));
            this.element = mainWidget;
        },
        //Skin Change at run time
        _changeSkin: function (skin) {
            if (this.model.cssClass != skin) {
                this.element.removeClass(this.model.cssClass).addClass(skin);
            }
        },
        //Enable avtion chnaged.
        _enabledAction: function (flag) {
            if (flag) {
                this.element.removeClass("e-disable");
                this._wireEvents();
            }
            else{
                this.element.addClass("e-disable");
                this._unWireEvents();
            }
        },

        // data source checking
        _checkDataBinding: function () {
            if (this.model.fields["dataSource"] instanceof ej.DataManager) {
                this._initDataSource();
            } else {
                this._ensureDataSource(this.model.fields["dataSource"]);
                this.initialize();
            }
        },

        _initDataSource: function () {
            this.element.ejWaitingPopup();
            var waitingPopup = this.element.ejWaitingPopup("instance");
            this.element.ejWaitingPopup("refresh");
            this.element.ejWaitingPopup("show");
            var query = this._columnToSelect(this.model.fields);
            var proxy = this;
            var queryPromise = this.model.fields["dataSource"].executeQuery(query);
            queryPromise.done(function (e) {
                proxy.element.ejWaitingPopup("hide");
                proxy.retriveData = e.result;
                proxy._ensureDataSource(e.result);
                proxy.initialize();
            });
        },

        //query check and create select based on the mapperField.

        _columnToSelect: function (mapper) {
            var column = [], queryManager = ej.Query();
            if (ej.isNullOrUndefined(mapper.query)) {
                for (var col in mapper) {
                    if (col !== "tableName" && col !== "child" && col !== "dataSource")
                        column.push(mapper[col]);
                }
                if (column.length > 0)
                    queryManager.select(column);
                if (!this.model.fields["dataSource"].dataSource.url.match(mapper.tableName + "$"))
                    !ej.isNullOrUndefined(mapper.tableName) && queryManager.from(mapper.tableName);
            }
            else
                queryManager = mapper.query;
            return queryManager;
        },

        // data source checking
        _ensureDataSource: function (dataSource) {
            this.currentSelectedData = dataSource;
            this.element.append(this._renderTemplate(dataSource));
        },

        //Render Treeview from template.

        _renderTemplate: function (item) {
            var ulTag = ej.buildTag("ul"), subItem = "";
            this._odataFlag = false;
            this._hasSelected = [];
            for (i = 0; i < item.length; i++) {
                if (item[i].selected)
                    this._hasSelected = item[i];
            }
            for (var i = 0; i < item.length; i++) {
                if (this.model.fields["dataSource"] != null) {
                    if (ej.isNullOrUndefined(item[i][this.model.fields.parentId]) || item[i][this.model.fields.parentId] == 0)
                        subItem = this._tempalteSub(item[i], item, this.model.fields);
                } else
                    subItem = this._tempalteSub(item[i], item, this.model.fields);
                ulTag.append(subItem);
            }
            return ulTag;
        },
        _tempalteSub: function (item, dataSource, mapper) {
            var liTag, spanTag, imgTag, aTag, childItems, ulTag, loadFlag = false;//, mapper = this.model.mapperField;
            if (this.model.fields["dataSource"] != null)
                if (this.model.loadOnDemand && item[mapper.hasChild]) {
                    childItems = ej.DataManager(this.currentSelectedData).executeLocal(ej.Query().where(mapper.parentId, ej.FilterOperators.equal, item[mapper.id]));
                    ulTag = $("<ul></ul>");
                    loadFlag = true;
                } else if (!ej.isNullOrUndefined(mapper.parentId))
                    childItems = ej.DataManager(this.currentSelectedData).executeLocal(ej.Query().where(mapper.parentId, ej.FilterOperators.equal, item[mapper.id]));
                else
                    childItems = item[mapper.childItems];
            if (ej.isNullOrUndefined(this.model.template)) {
                liTag = $('<li></li>');
                if (item[mapper.id])
                    liTag.prop("id", item[mapper.id]);
                if (item[mapper.htmlAttribute]) this._setAttributes(item[mapper.htmlAttribute], liTag);
                if (item[mapper.imageUrl]) {
                    imgTag = ej.buildTag('img.e-align');
                    imgTag.attr('src', item[mapper.imageUrl]);
                    if (item[mapper.imageAttribute]) this._setAttributes(item[mapper.imageAttribute], imgTag);
                    liTag.append(imgTag);
                }
                else if (item[mapper.spriteCssClass]) {
                    spanTag = ej.buildTag('span.' + item[mapper.spriteCssClass]);
                    liTag.append(spanTag);
                }
                if (item[mapper.text] != "") {
                    aTag = ej.buildTag('a', item[mapper.text], "", { href: "#" });
                    if (item[mapper.linkAttribute])
                        aTag.attr('href', item[mapper.linkAttribute]);
                    liTag.append(aTag);
                }
            }
            else {
                var divEle = $("<div></div>");
                divEle.append($("#" + this.model.template).render(item));
                liTag = divEle.find("li");
            }
            this._hasExpanded = false;
            if(item[mapper.expanded]) {
                liTag.addClass("expanded");
                this._hasExpanded = true;
            }
            item[mapper.isChecked] && liTag.addClass("ischecked");
            if(item[mapper.id]==this._hasSelected.id)
                aTag.addClass("e-text CanSelect e-active");
            if (loadFlag)
                liTag.append(ulTag);

            if (!ej.isNullOrUndefined(mapper["child"]) && !this.model.loadOnDemand) {
                this._odataFlag = true;
                if (mapper["child"]["dataSource"] instanceof ej.DataManager) {
                    var proxy = this, queryManager = ej.Query();
                    queryManager = this._columnToSelect(mapper["child"]);
                    queryManager.where(mapper["child"]["parentId"], ej.FilterOperators.equal, item[mapper.id]);
                    var queryPromise = mapper["child"]["dataSource"].executeQuery(queryManager);
                    queryPromise.done(function (e) {
                        childItems = e.result;
                        if (childItems && childItems.length > 0) {
                            proxy._createSubNodesWhenLoadOnDemand(childItems, liTag, mapper["child"]);
                            $(liTag).find(" >div >div").first().addClass("e-icon e-plus");
                        }
                    });
                }
                else {
                    var childItems = ej.DataManager(mapper["child"]["dataSource"]).executeLocal(ej.Query().where(mapper["child"]["parentId"], ej.FilterOperators.equal, item[mapper.id]));
                    if (childItems && childItems.length > 0) {
                        var ul = $(document.createElement('ul'));
                        for (var i = 0; i < childItems.length; i++) {
                            var liItem = this._tempalteSub(childItems[i], mapper["child"]["dataSource"], mapper["child"]);
                            ul.append(liItem);
                        }
                        liTag.append(ul);
                    }
                }
            }
            else if (this.model.loadOnDemand) {
                if (childItems && childItems.length > 0 && this._hasExpanded) {
                        for (var i = 0; i < childItems.length; i++) {
                            var liItem = this._tempalteSub(childItems[i], dataSource, mapper);
                            ulTag.append(liItem);
                        }
                        this._hasExpanded = false;
                        liTag.append(ulTag);
                }
            }
            else if (!this._odataFlag) {
                if (childItems && childItems.length > 0) {
                    var ul = ej.buildTag('ul');
                    for (var i = 0; i < childItems.length; i++) {
                        var liItem = this._tempalteSub(childItems[i], dataSource, mapper);
                        ul.append(liItem);
                    }
                    liTag.append(ul);
                }
            }
            return liTag;
        },
        _setAttributes: function (data, element) {
            for (var key in data) {
                element.attr(key, data[key]);
            }
        },
        _addDragableClass: function () {
            if (this.model.dragAndDrop) {
                this._anchors = this._liList.map(function () {
                    return $("a", this)[0];
                });
                this._anchors.addClass("e-draggable e-droppable");
            }
        },
        _addBaseClass: function () {
            this._ulList.addClass("e-treeview-ul").attr("role", "group");
            this._liList.addClass("e-item").attr("role", "treeitem");
            this.element.find("ul").first().removeClass("e-treeview-ul");
        },
        //Applies the autoformat
        _controlRender: function () {
            if (this.element != null) {
                var element = this.element.find("> ul");
                var licoll = element.find('li');
                var nodecount = -1;
                for (var i = 0; i < licoll.length; i++) {
                    var listEl = $(licoll[i]);
                    var textElement;
                    var customElement;
                    var nodeText;
                    //Check for sub-tree view items.Take back up and remove them
                    var subItems = listEl.children('ul')[0];
                    if (this.model.showCheckbox == true && $(subItems).length != 0) {
                        var parentli = element.find('>li');
                        parentli.each(function (liindex) {
                            if (($(subItems).parents('li.e-item').first())[0] == $(parentli[liindex])[0])
                                nodecount = nodecount + 1;
                        });
                        this._checkboxrecursive(element, subItems, nodecount);
                    }
                    if (subItems)
                        $(listEl.children('ul')[0]).remove();

                    //Check for text elements(a) and add class if any
                    var linkElement = listEl.children('a')[0];
                    if (linkElement) {
                        nodeText = $(linkElement).text();
                        textElement = $(linkElement).clone();
                        $(linkElement).remove();
                        $(textElement).addClass('e-text');
                        $(textElement).addClass('CanSelect');
                        customElement = listEl.clone();
                        listEl.html('');
                    }
                        //Otherwise build text elements(a)
                    else {
                        nodeText = this._getText(listEl);
                        customElement = listEl.clone();
                        listEl.html('');
                        textElement = ej.buildTag("a.e-text CanSelect", nodeText, "", { href: "#" + $.trim(nodeText), alt: ""});
                    }

                    //Span to show the images for Themes
                    var span = ej.buildTag('span');
                    if (subItems) {
                        $(span).addClass('collapseimg');
                    }
                    else {
                        $(span).addClass('child');
                    }

                    // Element to show expand, collapse image
                    var exCollpasediv = ej.buildTag('div', "", {}, { role: "presentation" });

                    //Outter div element 
                    var outerdiv = ej.buildTag('div', "", {}, { role: "presentation" });
                    $(outerdiv).append(exCollpasediv).append(span).append(customElement.children()).append(textElement);
                    listEl.prepend(outerdiv);
                    //Check for sub-tree view items and append them
                    if (subItems)
                        listEl.append(subItems);

                    //Check if ID is null for the node, then set the NodeText as ID
                    if (listEl.prop('name') == undefined) {
                        listEl.prop('name', nodeText);
                    }
                }
                if (this.model.showCheckbox) {
                    this._addCheckBox(element);
                    this.element.find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
                }
            }

            var liCollection = element.find('li');
            liCollection.find('>ul').hide();
            var acollection = liCollection.find('a');
            acollection.focus(function () {
                $(this).blur();
            });
            //Adding the Background position Class for all first and last Parent Nodes   
            liCollection.filter('li:last-child').addClass('last');
            $(liCollection[0]).addClass('first');

            if ($(liCollection[0]).hasClass('first') && $(liCollection[0]).hasClass('last'))
                $(liCollection[0]).removeClass('first');

            $(liCollection.filter(':has(ul)')).each(function () {
                $(this).attr("aria-expanded", false).attr("aria-selected", false)
                var liHasul = $($(this).children()).filter('ul');
                if ($(liHasul).is(':hidden')) {
                    $($(this).children().find('div')[0]).removeClass('e-icon e-minus').addClass('e-icon e-plus');
                }
                else {
                    $(this.childNodes[1]).removeClass('e-icon e-plus').addClass('e-icon e-minus');
                }
            });
        },

        _getText: function (element) {
            return $(element)
                      .clone()    //clone the element
                      .children() //select all the children
                      .remove()   //remove all the children
                      .end()  //again go back to selected element
                      .text();    //get the text of element
        },

        _finalize: function () {
            //Onfocus flag.
            this._isFocus = false;
            //Expand the specific nodes.
            var expandList = this.element.find(".expanded");
            for (var i = 0; i < expandList.length; i++) {
                this._expandNode(expandList[i]);
            }
            //is Checked checkbox element.
            this.model.showCheckbox && this._isCheckedAction();

            //Expanded Nodes
            for (var i = 0; i < this.model.expandedNodes.length; i++) {
                this._expandNode(this._liList[this.model.expandedNodes[i]]);
            }

            //Allow Editing on AllowEdit = true
            this.model.allowEdit && this._enableAllowEdit();

            $(this._liList[0]).find("a.e-text").attr("id", this._id + "_active");

            this.element.attr("tabindex", 0);
        },

        _isCheckedAction: function () {
            var checkedList = this.element.find(".ischecked");
            for (var chk = 0; chk < checkedList.length; chk++) {
                this._nodeCheckedAction($(checkedList[chk]).find("span.e-chkbox-wrap").first()); //e-checkbox
            }
            this.element.find(".ischecked").removeClass("ischecked");
        },
        //Add Check box
        _addCheckBox: function (element) {
            var parentli = element.find('>li');
            for (var i = 0; i < parentli.length; i++) {
                var checkbox = ej.buildTag("input.nodecheckbox", "", {}, { type: "checkbox", name: "TreeView_Checkbox" + i , value: "False" });

                //Create a hidden Checkbox
                var checkboxhdn = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox" + 1, value: i });

                $(parentli[i]).children('div').children('span').first().after($(checkboxhdn));
                $(parentli[i]).children('div').children('span').first().after($(checkbox));
            }
        },

        _checkboxrecursive: function (element, subItems, nodecount) {
            var proxy = this, nodeindex;
            var ulcollection = $(subItems).find('>li');

            if ($($($(subItems).parents('li.e-item')[0]).find('>input[type= hidden]')[0]).length == 0)
                nodeindex = nodecount;
            else
                nodeindex = $($(subItems).parents('li.e-item')[0]).find('>input[type=hidden]')[0].value;

            ulcollection.each(function (ulindex) {
                var nodecountvalue = nodecount;
                var nodeindexvalue = nodeindex + "." + ulindex;
                proxy._checkboxrecursive(nodeindex, this, nodecountvalue);
                var checkbox = ej.buildTag("input.nodecheckbox", "", {}, { type: "checkbox", name: "TreeView_Checkbox" + nodeindexvalue, value: "False" });
                //Create a hidden Checkbox
                var checkboxhdn = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox" + nodeindexvalue, value: nodeindexvalue });
                var liEle = $(ulcollection[ulindex]);
                if (liEle.find("a.e-text ").length > 0) {
                    checkbox.insertBefore(liEle.find("a.e-text ")[0]);
                    checkboxhdn.insertBefore(liEle.find("a.e-text ")[0]);
                } else {
                    liEle.prepend(checkbox).prepend(checkboxhdn);
                }
            });
        },

        _checkedChange: function (args) {
            var treeview = $(this.element).closest(".e-treeview").data("ejTreeView");
            if (args.isChecked)
                treeview._nodeCheckedAction(this.element)
            else
                treeview._nodeUnCheckedAction(this.element)
        },

        //Click event handler
        _ClickEventHandler: function (event) {
            var proxy = this;
            var element = event.target;
            if (!this._isclicked) {
                this._isclicked = true;
                this._onClick({ Event: event });
                if (element.tagName.toUpperCase() == 'DIV') {
                    if ($(element).hasClass('e-plus') || $(element).hasClass('e-minus')) {
                        var expandUl = null;
                        if (element.tagName.toUpperCase() == 'SPAN') {
                            expandUl = $(element.parentNode).parent().children().filter('ul');
                            element = $(element.parentNode).children().filter('div')[0];
                        }
                        else {
                            expandUl = $(element.parentNode).parent().children().filter('ul');
                        }
                        var isExpanded = $(expandUl).children().filter('li').length > 0 ? true : false;
                        if (!isExpanded && this.model.loadOnDemand) {
                            $(element).addClass("e-load");
                            var nodeid = $($(element).parents('li.e-item')[0]).attr('id');
                            var nodeText = $($($($(element).parents('li.e-item')[0]).children('div')[0]).children('a[class*=e-text]')[0]).text();
                            var args = { id: nodeid, value: nodeText };
                            this._loadOnDemand(args, element);
                        }
                        else
                            this._expandCollapseAction(element);
                        this._nodeSelectionAction($(element).parents('li.e-item').first().find('a[class*=e-text]').first());
                    }
                }
                if (element.tagName.toUpperCase() == 'A') {
                    $(element).hasClass('CanSelect') && !$(element).hasClass('e-active') && this._nodeSelectionAction(element);
                }
            }


            setTimeout(function () { proxy._isclicked = false; }, 300);
            if (element.tagName.toUpperCase() == 'A') {
                var target = element.href;
                (target.indexOf("#") != -1);
            }

        },

        _loadOnDemand: function (args, element) {
            var childItems, proxy = this;;
            if (this.model.fields["dataSource"] instanceof ej.DataManager) {
                var query = ej.Query().where(this.model.fields.parentId, ej.FilterOperators.equal, args.id);
                var queryPromise = this.model.fields["dataSource"].executeQuery(query);
                queryPromise.done(function (e) {
                    childItems = e.result;
                    if (childItems.length > 0) {
                        proxy._createSubNodesWhenLoadOnDemand(childItems, element, proxy.model.fields);
                        proxy._expandCollapseAction(element);
                    }
                });
            } else {
                childItems = ej.DataManager(this.currentSelectedData).executeLocal(ej.Query().where(this.model.fields.parentId, ej.FilterOperators.equal, args.id));
                (childItems.length > 0) && setTimeout(function () { proxy._createSubNodesWhenLoadOnDemand(childItems, element, proxy.model.fields); proxy._expandCollapseAction(element); }, 400);
            }
        },

        _getPath: function (element) {
            var path = $(element).prop('name');
            var ulelement = $(element).parents('li')[0];
            while (ulelement != null && ulelement.parentNode.id != this._id) {
                path = $(ulelement).prop('name') + '/' + path;
                ulelement = $(ulelement).parents('li')[0];
            }
            path = this._id + "/" + path;
            return path;
        },

        //Action on Node Selection
        _nodeSelectionAction: function (element) {
            if ($(element) == null) return;
            //var isExpanded = this._isExpanded(element);
            if ($($(element).parents('li')).hasClass('e-node-disable')) return;
            // remove previous selection
            $($(this.element).find('.e-active')).removeClass('e-active');
            $(this.element).find("#" + this._id + "_active").removeAttr("id");
            $(this.element).find('[aria-selected=true]').attr("aria-selected", false);
            $(element).addClass('e-active');
            var currentElement = $($(element).parents('li.e-item')[0]);
            currentElement.attr("aria-selected", true).find("a.e-text").attr("id", this._id + "_active");
            var nodeText = $(element).text();
            var data = { currentElement: currentElement, value: nodeText };
            this._onNodeSelect(data);

        },
        //Action on Node Selection
        _nodeUnSelectionAction: function (element) {
            if (element[0] == null) return;
            $(element).removeClass('e-active');
        },

        //Action on Node Selection
        _nodeEnableAction: function (element) {
            if (element[0] == null) return;
            element = $(element);
            this.model.showCheckbox && $($(element.parents('li.e-item')[0]).find('input')[0]).prop('disabled', false);
            $(element.parents('li.e-item')[0]).removeClass('e-node-disable');
            $(element.parents('li.e-item')[0]).find('a').removeClass('e-node-disable');
            $(element.parents('li.e-item')[0])[0].disabled = false;
        },

        //Action on Node Selection
        _nodeDisableAction: function (element) {
            if (element[0] == null) return;
            // collapse the parent node
            element = $(element);
            this._collpaseNode(element.parents('li.e-item')[0]);
            this.model.showCheckbox && $($(element.parents('li.e-item')[0]).find('input')[0]).prop('disabled', true);
            $(element.parents('li.e-item')[0]).addClass('e-node-disable');
            $(element.parents('li.e-item')[0]).find('a').addClass('e-node-disable');
            if ($(element.parents('li.e-item')[0]).find('a').hasClass('e-active'))
                $(element.parents('li.e-item')[0]).find('a').removeClass('e-active');
            $(element.parents('li.e-item')[0])[0].disabled = true;
        },
        //Expands or collapse the selected node
        _expandCollapseAction: function (element) {
            var expandUl;
            var proxy = this;
            if ($(element.parentNode).parent().hasClass('e-node-disable')) return;
            if (!ej.isNullOrUndefined(element.tagName) && element.tagName.toUpperCase() == 'SPAN') {
                expandUl = $(element.parentNode).parent().children().filter('ul');
                element = $(element.parentNode).children().filter('div')[0];
            }
            else {
                expandUl = $(element.parentNode).parent().children().filter('ul');
            }
            var elmt;
            if (element.tagName == "DIV")
                elmt = $(element.parentNode.parentNode)[0];
            else
                elmt = element;
            var liElement = $($(element).parents('li.e-item')[0]);
            if (!this._isExpanded(elmt)) {
                liElement.attr("aria-expanded", true).attr("tabindex", 0);
                var nodeText = $($($($(element).parents('li.e-item')[0]).children('div')[0]).children('a[class*=e-text]')[0]).text();
                var data = { currentElement: liElement, value: nodeText };
                if (this._onBeforeExpand(data) === true)
                    return false;
                $(expandUl).animate({ height: 'toggle' }, 350, 'linear', function () {
                    proxy._onNodeExpand(data);
                    proxy._addExpandedNodes(proxy._liList.index(liElement));
                });
                $(element).removeClass('e-icon e-plus').addClass('e-icon e-minus');
                $(element.parentNode.parentNode).addClass('e-collapse');
                $($(element.parentNode).children()[1]).removeClass('collapseimg').addClass('expandimg');
            }
            else {
                liElement.attr("aria-expanded", false).attr("tabindex", -1);
                var nodeText = $($($($(element).parents('li.e-item')[0]).children('div')[0]).children('a[class*=e-text]')[0]).text();
                var data = { currentElement: $($(element).parents('li.e-item')[0]), value: nodeText };
                if (this._onBeforeCollapse(data) === true)
                    return false;
                $(expandUl).animate({ height: 'toggle' }, 200, 'linear', function () {
                    proxy._onNodeCollapse(data);
                    proxy._removeExpandedNodes(proxy._liList.index(data.currentElement));
                });
                $(element).removeClass('e-icon e-minus').addClass('e-icon e-plus');
                if (element.parentNode != null) {
                    $(element.parentNode.parentNode).removeClass('e-collapse');
                    $($(element.parentNode).children()[1]).addClass('collapseimg').removeClass('expandimg');
                }
            }
        },

        _isExpanded: function (element) {
            if (element.tagName == 'A')
                element = $(element.parentNode.parentNode)[0];
            var isExpanded = this.isNodeExpanded(element);
            if ($($(element).children().filter("ul")) != null)
                if ($($(element).children().filter("ul").find('li')) != null)
                    isExpanded = $($(element).children().filter("ul").find('li')).is(':not(:hidden)');
                else
                    isExpanded = false;
            return isExpanded;
        },
        _isChecked: function (element) {
            var isChecked;
            if ($(element).val() === "True")
                isChecked = true;
            else
                isChecked = false;
            return isChecked;
        },

        _nodeCheckedAction: function (element) {
            //var checkboxClass = element.className;
           
            var proxy = this;
            var chkEle = $($(element).find('input.nodecheckbox[type=checkbox]')[0]);
            chkEle.addClass("checked").prop('value', 'True');
            chkEle.ejCheckBox({ "indeterminateState": false, "indeterminate": false, "check": true });
            this._onChecked(chkEle);
            $($(element).parents('li.e-item')[0]).children('ul').find('input.nodecheckbox[type=checkbox]').prop('checked', true).addClass("checked").prop('value', 'True');
            var chkCollection = $($(element).parents('li.e-item')[0]).children('ul').find('input.nodecheckbox[type=checkbox]');
            chkCollection.each(function (index) {
                $(this).ejCheckBox({ "check": true });
                // Client side checked events
                proxy._onChecked(this);
            });
            var autoCheckParentNode = this.model.autoCheckParentNode;
            var parentlicollection = $($(element).parents('li.e-item')[0]).parents('li.e-item');
            parentlicollection.each(function (index) {
                var childulcollection = $(parentlicollection[index]).children('ul').find('li.e-item');
                var count = $(parentlicollection[index]).children('ul').find('li.e-item').find('input.nodecheckbox[value="True"]').length;
                var chkEle = $($(parentlicollection[index]).find('input.nodecheckbox[type=checkbox]')[0]);
                if (count == childulcollection.length || autoCheckParentNode) {
                    chkEle.addClass("checked").prop('value', 'True');
                    $(chkEle).ejCheckBox({ "indeterminateState": false });
                    $(chkEle).ejCheckBox({ "indeterminate": false, "check": true, });
                    proxy._onChecked(chkEle);
                } else {
                    chkEle.ejCheckBox({ "indeterminate": true });
                    chkEle.ejCheckBox({ "indeterminateState": true });
                    chkEle.val("False");
                }
            });
            var checkedcoll = this.element.find('input.nodecheckbox[value="True"]');
            for (var i = 0; i < checkedcoll.length; i++) {
                var chkEle = $(checkedcoll[i]).parents('.e-chkbox-wrap');
                var value = $(chkEle.next()[0])[0].value;
                var nodeid = $($(checkedcoll[i]).parents('li.e-item')[0]).attr('id');
                var text = $(checkedcoll[i]).parents("li.e-item").first().find(".e-text").first().text();

                var checkboxhdnvalue = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox" + value + ".Value", value: nodeid });
                //Create a hidden Checkbox
                var checkboxhdntext = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox" + value + ".Text", value: text });
                try {
                    $(checkedcoll[i]).append($(checkboxhdnvalue));
                    $(checkedcoll[i]).append($(checkboxhdntext));
                }
                catch (err) {
                }
            }
        },
        _nodeUnCheckedAction: function (element) {
            var proxy = this;
            element = $(element);
            var chkEle = $($(element).find('input.nodecheckbox[type=checkbox]')[0]);
            chkEle.removeClass('checked').prop('value', 'False').children().remove();
            chkEle.ejCheckBox({ "indeterminate": false, "check": false });
            this._onUnChecked(chkEle);
            var unCheckedList = $(element.parents('li.e-item')[0]).children('ul').find('input.nodecheckbox[type=checkbox]');
            unCheckedList.removeClass("checked").prop('value', 'False').children().remove();
            unCheckedList.each(function (index) {
                $(this).ejCheckBox({ "check": false });
                proxy._onUnChecked(this);
            });
            var autoCheckParentNode = this.model.autoCheckParentNode;
            var parentlicollection = $(element.parents('li.e-item')[0]).parents('li.e-item');
            for (var index = 0; index < parentlicollection.length; index++) {
                var count = $(parentlicollection[index]).find('input.nodecheckbox[value="True"]').length;
                var chkEle = $($(parentlicollection[index]).find('input.nodecheckbox[type=checkbox]')[0]);
                if (count <= 0) {
                    chkEle.ejCheckBox({ "indeterminateState": false });
                    chkEle.ejCheckBox({ "indeterminate": false, "check": false });
                    //Client side unchecked event
                    this._onUnChecked(chkEle);
                }
                if (count > 1 && autoCheckParentNode) {
                    chkEle.addClass("checked").prop('value', 'True');
                    chkEle.ejCheckBox({ "check": true });
                }
                else if (count == 1 && autoCheckParentNode) {
                    chkEle.addClass("checked").prop('value', 'False');
                    chkEle.ejCheckBox({ "check": false });
                }
                else if (count > 0) {
                    chkEle.removeClass('checked').prop('value', 'False').children().remove();
                    chkEle.ejCheckBox({ "indeterminate": true });
                    chkEle.ejCheckBox({ "indeterminateState": true });
                }
            }
        },

        //Enables Edit
        _enableAllowEdit: function () {
            var element = this.element;
            var liCollection = $(element).find('li');
            liCollection.each(function () {
                $(this).addClass('AllowEdit');
            });
        },

        //Disables Edit
        _disableAllowEdit: function () {
            var liCollection = this.element.find('li');
            liCollection.each(function () {
                $(this).removeClass('AllowEdit');
            });
        },

        _expandNode: function (value) {
            if (value != null) {
                //if ($(element).find('ul').length > 0) {
                if (this.model.loadOnDemand)
                    this._expandCollapseAction($($(value).children('div')).children()[0]);
                else {
                if ($($($(value).children('div')).children()[0]).hasClass('e-plus')) {
                    var isExpanded = $(value).children('ul').find('li').length > 0 ? true : false;
                    if (isExpanded) {
                        this._expandCollapseAction($($(value).children('div')).children()[0]);
                    }
                }
            }
            }
            return true;
        },

        //Collapses the selected node
        _collpaseNode: function (value) {
            var element = value;
            if (element != null) {
                if ($(element).find('ul').length > 0) {
                    if ($($($(element).children('div')).children()[0]).hasClass('e-minus')) {
                        this._expandCollapseAction($($(element).children('div')).children()[0]);
                    }
                }
            }
        },

        //Expands all the nodes
        _expandAll: function () {
            var proxy = this;
            var element = this.element;
            var liCollection = $(element).find("li:not([class*='e-node-disable'])");
            var collapsedNodes = liCollection.find('>ul:hidden').parents('li.e-item');
            if (liCollection.find('>ul:hidden').find("li").length > 0)
                liCollection.find('>ul:hidden').animate({ height: 'toggle' }, 350);
            $(collapsedNodes).each(function () {
                if (!$(this).hasClass("e-node-disable")) {
                    var liElement = $($(this).parents('li.e-item')[0]);
                    var nodeText = $($(this).find(">div >a")[0]).text();
                    var data = { currentElement: liElement, value: nodeText };
                    if (proxy._onBeforeExpand(data) === true)
                        return false;
                    if ($(this).find('li').length > 0) {
                        $(this).addClass('expand');
                        $($($(this).children('div')[0]).children()[1]).removeClass('collapseimg').addClass('expandimg');
                        $($($(this).children('div')[0]).children()[0]).removeClass('e-icon e-plus').addClass('e-icon e-minus');
                    }
                    proxy._onNodeExpand(data);
                }
            });
        },

        //Collapse all the nodes
        _collapseAll: function () {
            var liCollection = this.element.find('li');
            this._collapseNodes(liCollection);
        },
        //Check all the nodes of the checkbox
        _checkAll: function () {
            if (this.model.showCheckbox) {
                var element = this.element;
                var liCollection = $(element).find("li");
                for (var i = 0; i < liCollection.length; i++) {
                    var checkboxelement = $(liCollection[i]).children().find('input.nodecheckbox')[0];
                    if ($(checkboxelement)[0].disabled)
                        $(checkboxelement).ejCheckBox("setModel", { check: false });
                    else {
                        $(checkboxelement).ejCheckBox("setModel", { check: true });
                        $(checkboxelement).attr("value", "True");
                        this._onChecked(checkboxelement);
                    }
                }
            }
            else
                alert("Please Enable the ShowCheckbox property");
        },
        _unCheckAll: function () {
            if (this.model.showCheckbox) {
                var element = this.element;
                var liCollection = $(element).find("li");
                for (var i = 0; i < liCollection.length; i++) {
                    var chkEle = $(liCollection[i]).find('input.nodecheckbox')[0];
                    $(chkEle).ejCheckBox("setModel", { check: false });
                    $(chkEle).attr("value", "False");
                    this._onUnChecked(chkEle);
                }
            }
            else
                alert("Please Enable the ShowCheckbox property");
        },

        _collapseNodes: function (liCollection) {
            var proxy = this;
            var _expandedNodes = liCollection.find('>ul:not(:hidden)').parents('li.e-item');
            liCollection.find('>ul:not(:hidden)').animate({ height: 'toggle' }, 350);
            $(_expandedNodes).each(function () {
                var nodeText = $($(this).find(">div >a")[0]).text();
                var data = { currentElement: this, value: nodeText };
                if (proxy._onBeforeCollapse(data) == true)
                    return false;
                //var isExpanded = false;
                if ($(this).find('li').length > 0) {
                    $(this).removeClass('e-collapse');
                    $($($(this).children('div')[0]).children()[1]).removeClass('expandimg').addClass('collapseimg');
                    $($($(this).children('div')[0]).children()[0]).removeClass('e-icon e-minus').addClass('e-icon e-plus');
                }
                proxy._onNodeCollapse(data);
            });
        },
        _showCheckBox: function () {
            var element = this.element.find("> ul");
            var licoll = element.find('li');
            for (var i = 0; i < licoll.length; i++) {
                var nodecount = -1;
                var listEl = licoll[i];
                //Check for sub-tree view items.Take back up and remove them
                var subItems = $(listEl).children('ul')[0];
                if (this.model.showCheckbox == true && $(subItems).length != 0) {
                    var parentli = element.find('>li');
                    parentli.each(function (liindex) {
                        if ($($(subItems).parents('li.e-item')[0])[0].id == $(parentli[liindex])[0].id)
                            nodecount = nodecount + 1;
                    });
                    this._checkboxrecursive(element, subItems, nodecount);
                }
            }
            this._addCheckBox(element);
            element.find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
        },

        //Drag and Drop Events handlers

        //dragEventHandler
        _drag: function () {
            var proxy = this;
            var pre = false;
            var _clonedElement = null;
            var dragContainment = null;
            this._treeView = this.element.parent();
            if (!this.model.dragAndDropAcrossControl)
                dragContainment = this.element.parent();
            //else
            //    dragContainment = this.element.parent();
            $(this._treeView).find("ul li div a").ejDraggable({
                dragArea: dragContainment,
                clone: true,
                dragStart: function (args) {
                    var data = { target: args.target };
                    if (proxy._onDragStarts(data)) {
                        args.cancel = true;
                        _clonedElement && _clonedElement.remove();
                        return false;
                    }
                },
                drag: function (args) {
                    $(".e-sibling").remove();
                    var target = args.target;
                    var data = { target: target };
                    if (proxy._onDrag(data))
                        return false;
                    if ($(target).hasClass('e-droppable') || $(target).parent().hasClass('e-droppable')) {
                        document.body.style.cursor = '';
                        $(target).addClass("allowDrop");
                    }
                    else {
                        document.body.style.cursor = 'not-allowed';
                        $(target).removeClass('showline-hover');
                        $(target).removeClass('noline-hover');
                    }
                    if (target.nodeName != 'A') {
                        if (target.nodeName == 'UL')
                            target = $(target).children()[0];
                        if (target.nodeName != 'LI')
                            target = $(target).parent('.e-droppable')[0] || $(target).parent();
                        if (target.nodeName == 'LI' && $(target).hasClass("e-droppable") && proxy.model.dropSibling) {
                            var targetY = $(".e-treeview").has(target).offset().top + $(target).children().find('a')[0].offsetTop || -1;
                            var div = ej.buildTag('div.e-sibling');
                            if ($(target).hasClass("last")) {
                                $(target).append(div);
                            }
                            else if ($(target).hasClass("first"))
                                $(target).prepend(div);
                            else {
                                pre = args.event.pageY < targetY ? false : true;
                                pre ? $(target).prepend(div) : $(target).append(div);
                            }
                        }
                    }
                    else
                        $(".e-sibling").remove();
                },
                dragStop: function (args) {
                    if (!args.element.dropped) {
                        _clonedElement && _clonedElement.remove();
                        document.body.style.cursor = '';
                    }
                    var target = args.target;
                    if (target.className == "e-sibling") {
                        target = $(target).parent("li")[0];
                    }
                    var targetObj = proxy;
                    var position = pre ? "Before" : "After";
                    position = target.nodeName == 'A' ? "Over" : position;
                    var data = { target: target, position: position };
                    if (proxy._onDragStop(data))
                        return false;
                    $(args.element).removeClass("e-active");
                    if (target.nodeName == 'A' && proxy.model.dropChild && $(target).hasClass('e-droppable') || (target.nodeName == 'UL' && $(target).children().length == 0)) {
                        position = "Over";
                        if (($(args.element).parent().parent().has($(target)).length == 0) && ($(target).parent().parent().has($(args.element)).length == 0 || proxy._isDescendant($(target).parents("li:last").find('>ul>li'), $(args.element).parents("li:first")[0]))) {
                            proxy._dropAsChildNode(target, args.element, args.event);
                            targetObj._applyFirstLastChildClass();
                        }
                    }
                    else {
                        if (target.nodeName == 'UL')
                            target = $(target).children()[0];
                        if (target.nodeName != 'LI')
                            target = $(target).parent('.e-droppable')[0] || $(target).parent();
                        if (target.nodeName == 'LI' && proxy.model.dropSibling && $(target).hasClass('e-droppable')) {
                            $(".e-sibling").remove();
                            if ($(args.element).parent().parent().has($(target)).length < 1) {
                                proxy._dropAsSublingNode(target, args.element, pre, args.event);
                                targetObj._applyFirstLastChildClass();
                            }
                        }
                    }
                    $(".e-sibling").remove();
                    $(".allowDrop").removeClass("allowDrop");
                    if (!proxy.model.dropChild) {
                        _clonedElement && _clonedElement.remove();
                    }
                    var data = { target: target, event: args.event, dropedElement: args.element };
                    if (proxy._onDropped(data))
                        return false;
                    document.body.style.cursor = '';
                },
                helper: function (event, ui) {
                    _clonedElement = $(event.sender.target).clone().addClass("dragClone");
                    return _clonedElement.appendTo($("body"));
                }
            });
        },
        _isDescendant: function (src, target) {
            var match = true;
            $(src).each(function (i, item) {
                if (item == target) {
                    match = false;
                    return false;
                }
                else
                    match = true;
            });
            return match;
        },
        //DropChildEventHandler
        _childDrop: function () {
            $(this._treeView).find("ul li div a").ejDroppable({
                accept: $(this._treeView).find("ul li div a"),
                drop: function (event, ui) {
                    $(ui.helper).hide();
                }
            });
        },
        //DropSiblingEventHandler
        _siblingDrop: function () {
            $(this._treeView).find("ul li").ejDroppable({
                drop: function (event, ui) {
                    $(ui.helper).hide();
                }
            });
        },

        //DropAsSubling
        _dropAsSublingNode: function (target, element, pre, event) {
            var li = event.ctrlKey ? $(element).parent().parent().clone() : $(element).parent().parent();
            var parentNode = $(element).parents('li.e-item')[1];
            pre ? $(li).insertBefore(target) : $(li).insertAfter(target);
            this._checkBox(target, li);
            this._modifiCss(parentNode);
            this._applyFirstLastChildClass();
        },
        //DropAsChild
        _dropAsChildNode: function (target, element, event) {
            var li = event.ctrlKey ? $(element).parent().parent().clone() : $(element).parent().parent();
            var parentNode = $(element).parents('li.e-item')[1];
            if (target.nodeName == 'UL')
                $(target).append(li);
            else
                this._appendNode(target, li);
            this._checkBox($(target).parent().parent(), li);
            this._modifiCss(parentNode);
            this._applyFirstLastChildClass();
            this._expandNode($(target).parent().parent()[0]);
        },
        _appendNode: function (selectedNode, outerLi) {
            if (this._showline && selectedNode.length != 0) {
                if ($($($(selectedNode).parents('li.e-item')[0]).find('li')).hasClass('last'))
                    $($($(selectedNode).parents('li.e-item')[0]).find('li')).removeClass('last');
                $(outerLi).addClass('last');
            }
            if (selectedNode.length != 0) {
                if ($($(selectedNode).parents('li.e-item')[0]).find('ul')[0] == null) {
                    var outerUl = ej.buildTag('ul.e-treeview-ul');
                    $(outerUl).append($(outerLi));
                    if (!$($($(selectedNode).parents('li.e-item')[0]).find('div')[1]).hasClass('e-minus')) {
                        $($($(selectedNode).parents('li.e-item')[0]).find('div')[1]).addClass('e-icon e-minus');
                    }
                    $($(selectedNode).parents('li.e-item')[0]).append($(outerUl));
                }
                else
                    $($($(selectedNode).parents('li.e-item')[0]).find('ul')[0]).append($(outerLi));
            }
        },

        _checkBox: function (target, li) {
            if (!$(li).hasClass("nodecheckbox") && $(li).find(".nodecheckbox").length > 0) {
                $(li).find(".e-chkbox-wrap").next().remove();
                $(li).find(".e-chkbox-wrap").remove();
            }
            if ($(target).find(".e-checkbox").length > 0) {
                var element = $(".e-treeview").has(target).find('ul')[0];
                $(element).find(".e-checkbox").next().remove();
                $(element).find(".e-checkbox").remove();
                var context = this;
                var nodecount = -1;
                var licoll = $(element).find('li');
                licoll.each(function (index) {
                    var subItems = $(this).children('ul')[0];
                    if ($(subItems).length != 0) {
                        var parentli = $(element).find('>li');
                        parentli.each(function (liindex) {
                            if ($($(subItems).parents('li.e-item')[0])[0].id == $(parentli[liindex])[0].id)
                                nodecount = nodecount + 1;
                        });
                        context._childcheckbox(element, subItems, nodecount);
                    }
                });
                this._addCheckBox($(element));
                this.element.find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
                $(target).closest(".e-treeview").find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
            }
        },
        _childcheckbox: function (element, subItems, nodecount) {
            var proxy = this, nodeindex;
            var ulcollection = $(subItems).find('>li');

            if ($($($($(subItems).parents('li.e-item')[0]).children()[0]).find('>input[type= hidden]')[0]).length == 0)
                nodeindex = nodecount;
            else
                nodeindex = $($($(subItems).parents('li.e-item')[0]).children()[0]).find('>input[type= hidden]')[0].value;

            ulcollection.each(function (ulindex) {
                var nodecountvalue = nodecount;
                var nodeindexvalue = nodeindex + "." + ulindex;
                proxy._childcheckbox(nodeindex, this, nodecountvalue);

                var checkbox = ej.buildTag("input.nodecheckbox", "", {}, { type: "checkbox", name: "TreeView_Checkbox" + nodeindexvalue, value: "False" });

                //Create a hidden Checkbox
                var checkboxhdn = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox.Index", value: nodeindexvalue });

                $(ulcollection[ulindex]).children('div').children('span').after($(checkboxhdn));
                $(ulcollection[ulindex]).children('div').children('span').after($(checkbox));
            });
        },
        _modifiCss: function (parentNode) {
            if ($(parentNode).find('li').length == 0) {
                $(parentNode).find('ul').remove();
                $(parentNode).removeClass('e-collapse');
                $(parentNode).find('div.e-icon').removeClass('e-icon e-minus');
            }
            else {
                if (!$($(parentNode).find('li')[$(parentNode).find('li').length - 1]).hasClass('last'))
                    $($(parentNode).find('li')[$(parentNode).find('li').length - 1]).addClass('last');
            }
        },

        _applyFirstLastChildClass: function () {
            var element = this.element;
            var liCollection = element.find('li');
            liCollection.removeClass('first');
            liCollection.removeClass('last');
            liCollection.filter('li:last-child').addClass('last');
            $(liCollection[0]).addClass('first');
            if ($(liCollection[0]).hasClass('first') && $(liCollection[0]).hasClass('last'))
                $(liCollection[0]).removeClass('first');
        },

        _expandEventHandler: function (event) {
            var selectedNode = $($(event.target).siblings('div'));
            if (!(event.type === "dblclick" && this.model.allowEdit)) {
                event.preventDefault();
                if (this.model.loadOnDemand) {
                    var element = $(event.target);
                    var expand = element.parents("li").find("div.e-icon e-plus,div.e-icon e-minus").first();
                    if (expand.hasClass("e-plus") && element.parents("li").find("ul >li").length == 0) {
                        expand.addClass("e-load");
                        var nodeid = $(element.parents('li.e-item')[0]).attr('id');
                        var nodeText = element.text();
                        var args = { id: nodeid, value: nodeText };
                        this._loadOnDemand(args, expand[0]);
                    }
                    else
                        this._expandCollapseAction(selectedNode[0]);
                }
                else {
                    if (selectedNode && (selectedNode.hasClass('e-plus') || selectedNode.hasClass('e-minus'))) {
                        this._expandCollapseAction(selectedNode[0]);
                    }
                }
            }
        },

        //Double Click Handler
        _inlineEdit: function (event) {
            event.preventDefault();
            if (event.target.tagName == 'A' && !$(event.target).hasClass("e-node-disable")) {
                if ($($(event.target).parents('li.e-item')[0]).hasClass('AllowEdit')) {
                    if ($(event.target).hasClass('e-text')) {
                        $(event.target).removeClass('e-active');
                        this._inlineEditAction(event.target);
                    }
                }
            }
            return false;
        },

        //Node Editing 
        _inlineEditAction: function (element) {

            var editTextBox = $('#Edit_Input')[0];
            if (editTextBox == null) {
                this._createEditTextBox(element);
            }
            else {
                editTextBox = $('#Edit_Input')[0];
                $(editTextBox.parentNode).html(editTextBox.value);
                $(editTextBox).remove();
                this._createEditTextBox(element);
            }
            $(element).css("display", "none");
            //this.set_Selected(true);
        },

        //Creates Edit texbox
        _createEditTextBox: function (values) {
            var argsData = { currentNode: values };
            if (this._trigger("beforeEdit", argsData))
                return false;
            var editTextBox = $('#Edit_Input')[0];
            if (editTextBox == null) {
                var textBox = ej.buildTag('Input.Input_Text#Edit_Input', "", "", { type: 'text', value: $.trim(values.innerHTML).replace(/\n\s+/g, " "), name: 'inplace_value' });
                $(textBox).width($(values).outerWidth() + 5);
                $(textBox).height($(values).outerHeight() - 2);
                this.beforeEditText = $(values).text();
                $(values).html('');
                $(values).before(textBox);
                var size = 0;
                editTextBox = $('#Edit_Input')[0];
                if (editTextBox.value.length == '')
                    size = 3;
                else
                    size = $(values).outerWidth() + 20;

                this._mousePositionAtEnd(editTextBox);
                this._currentEditableNode = values;
                var txtbox = $(editTextBox);
                //Keypress event handler for the edit textbox
                this._on(txtbox, 'keypress', this._editTextBoxKeyPress);
                //Keydown event handler for edit textbox
                this._on(txtbox, 'keydown', this, this._pressEscKey);

                // Focus out event handler for the edit textbox
                this._on(txtbox, 'blur', this._focusout);
            }
            return editTextBox;
        },

        _editTextBoxKeyPress: function (event) {
            event.target.size = event.target.value.length + 1;
        },

        _mousePositionAtEnd: function (ctl) {
            if (ctl.focus)
                ctl.focus();
            if (ctl.select)
                ctl.select();
            return true;
        },

        //Focus out event handler for Edit textbox
        _focusout: function (e) {

            var editTextBox = $('#Edit_Input')[0];
            var currentText = editTextBox.value;
            var id = $($(editTextBox).parents('li.e-item')[0]).attr('id');
            var requestArgs = {
                NodeID: id,
                OriginalText: this.beforeEditText,
                ModifiedText: currentText,
                UpdateChanges: true
            };

            if (this._trigger("inlineEditValidation", requestArgs) == true) {
                this._cancelAction(editTextBox);
            }
            else
                this._saveAction(editTextBox);
        },


        //Keydown event handler for edit textbox
        _pressEscKey: function (event) {
            event.cancelBubble = true;
            event.returnValue = false;
            var editTextBox = $('#Edit_Input')[0];

            if (editTextBox != null) {
                if (event.keyCode == 13) {
                    this._focusout();
                }

                if (event.keyCode == 27)
                    this._cancelAction(editTextBox);
            }
        },
        _onFocusHandler: function (event) {
            if (this.model.allowKeyboardNavigation && this._isFocus) {
                this.element.find(".e-active").removeClass("e-active");
                nextElement = this.element.find('li').first().find('a').first();
                $(nextElement).addClass('e-active');
                event.preventDefault();
                this._isFocus = false;
            }
        },

        //Key down event handler
        _KeyPress: function (e) {
            var code, node;
            var proxy = this;
            this._isFocus = true;
            var element = this.element.find("> ul");
            if (e.keyCode) code = e.keyCode; // ie and mozilla/gecko
            else if (e.which) code = e.which; // ns4 and opera
            else code = e.charCode;
            if (code == 113) {
                e.preventDefault();
                this._isFocus = false;
                var currentlySelectedItem = element.find(".e-active")[0];
                if (currentlySelectedItem != null) {
                    if ($($(currentlySelectedItem).parents('li.e-item')[0]).hasClass('AllowEdit')) {
                        $(currentlySelectedItem).removeClass('e-active');
                        this._createEditTextBox(currentlySelectedItem);
                    }
                }
            }
            if (proxy.model.allowKeyboardNavigation && (proxy.element.find('#Edit_Input').length < 1)) {
                var nextElement, selectedItem;
                selectedItem = $($(element).find('.e-active').parents('li.e-item')[0]);
                var liVisible = $(element).find('li').not(':hidden');
                // Down arrow key(next node Move)
                if (code == 40 && !e.ctrlKey && !e.altKey && !e.shiftKey) {
                    if (selectedItem.length > 0) {
                        e.preventDefault();
                        for (var i = 0; i < liVisible.length; i++) {
                            if ($($(liVisible[i]).find('a')[0]).hasClass('e-active')) {
                                if (i == liVisible.length - 1)
                                    this._nodeSelectionAction($(selectedItem.find('a')[0]).addClass('e-active'));
                                else {
                                    $($(liVisible[i]).find('a')[0]).removeClass('e-active');
                                    this._nodeSelectionAction($($(liVisible[i + 1]).find('a')[0]));
                                    break;
                                }
                            }
                        }
                        proxy._KeyPressEventHandler(nextElement, proxy, code);
                    }
                }
                    // Up arrow key(next node Move)
                else if (code == 38 && !e.ctrlKey && !e.altKey && !e.shiftKey) {
                    if (selectedItem.length > 0) {
                        e.preventDefault();
                        for (var i = 0; i < liVisible.length; i++) {
                            if ($($(liVisible[i]).find('a')[0]).hasClass('e-active')) {
                                if (i == 0)
                                    this._nodeSelectionAction($(selectedItem.find('a')[0]).addClass('e-active'));
                                else {
                                    $($(liVisible[i]).find('a')[0]).removeClass('e-active');
                                    this._nodeSelectionAction($($(liVisible[i - 1]).find('a')[0]).addClass('e-active'));
                                    break;
                                }
                            }
                        }
                        proxy._KeyPressEventHandler(nextElement, proxy, code);
                    }
                }

                    //Right arrow key(Expand)
                else if (code == 39 && !e.ctrlKey && !e.altKey && !e.shiftKey) {
                    if ($(selectedItem).children('div').find('div').hasClass('e-plus')) {
                        e.preventDefault();
                        var isExpanded = $(selectedItem).children('ul').find('li').length > 0 ? true : false;
                        if (!isExpanded && this.model.loadOnDemand) {
                            var element = $(selectedItem).find("div.e-icon e-plus,div.e-icon e-minus").first()
                            element.addClass("e-load");
                            var nodeid = $(selectedItem).attr('id');
                            var nodeText = $(selectedItem).find('a[class*=e-text]').first().text();
                            var args = { id: nodeid, value: nodeText };
                            this._loadOnDemand(args, element[0]);
                        }
                        else
                            this._expandNode(selectedItem);
                    }
                    else {
                        if ($(selectedItem).find("div.e-icon e-plus,div.e-icon e-minus").first().length > 0)
                            this.selectNode($(selectedItem).children("ul").find("li").first()[0]);
                        else {
                            var child = $(selectedItem).parent().find("li");
                            var index = child.index(selectedItem);
                            !ej.isNullOrUndefined(child[index + 1]) && $(selectedItem).children("ul").length > 0 && this.selectNode(child[index + 1]);
                        }
                    }
                    nextElement = $(selectedItem.find('a')[0]);
                    proxy._KeyPressEventHandler(nextElement, proxy, code);
                }

                    //Left arrow key(Collapse)
                else if (code == 37 && !e.ctrlKey && !e.altKey && !e.shiftKey) {
                    if ($(selectedItem).children('div').find('div').hasClass('e-minus')) {
                        e.preventDefault();
                        this._collpaseNode(selectedItem);
                    }
                    else if ($(selectedItem).parents().first().parents("li").first().length > 0) {
                        $(selectedItem).find("a.e-active").first().removeClass('e-active');
                        $(selectedItem).parents().first().parents("li").first().find('a').first().addClass("e-active");

                    }
                    nextElement = $(selectedItem.find('a')[0]);
                    proxy._KeyPressEventHandler(nextElement, proxy, code);
                }

                    //Enter Key(Expand and Collapse)
                else if (code == 13) {
                    if (selectedItem.length > 0) {
                        if ($(selectedItem).children('div').find('div').hasClass('e-minus'))
                            this._collpaseNode(selectedItem);
                        else
                            this._expandNode(selectedItem);
                        nextElement = $(selectedItem.find('a')[0]);
                        proxy._KeyPressEventHandler(nextElement, proxy, code);
                    }
                }
                    //Space Key(toCheck and Uncheck node)
                else if (code == 32) {
                    if (selectedItem.length > 0 && this.model.showCheckbox) {
                        e.preventDefault();
                        var chkBoxEle = selectedItem.find('input.nodecheckbox[type=checkbox]').first();
                        if (chkBoxEle[0].checked)
                            this._nodeUnCheckedAction(chkBoxEle.parent().parent());
                        else
                            this._nodeCheckedAction(chkBoxEle.parent().parent());
                    }
                }
                    //Del key to delete the e-active node
                else if (code == 46) {
                    if (selectedItem.length > 0) {
                        e.preventDefault();
                        this._removeNode(selectedItem.find("a.e-text").first());
                    }
                }
                else if (e && e.ctrlKey == true) {
                    //Cut the node
                    if (code == 88) {
                        var cutNodeLi;
                        if ($(selectedItem).hasClass('first')) {
                            cutNodeLi = $(selectedItem).removeClass('first').addClass('last');
                            $(selectedItem).next().addClass('first');
                        }
                        else if (!$(selectedItem).hasClass('last'))
                            cutNodeLi = $(selectedItem).addClass('last');
                        else {
                            cutNodeLi = $(selectedItem);
                            $(selectedItem).prev().addClass('last');
                        }
                        this._cutCopyNodeText = cutNodeLi;
                        proxy._removeNode();
                        this.element.focus();
                    }

                        //Copy the node
                    else if (code == 67) {
                        var copyNodeLi = $(selectedItem).clone();
                        this._cutCopyNodeText = copyNodeLi;
                    }
                        // Paste the node
                    else if (code == 86 && this._cutCopyNodeText != null) {
                        $(selectedItem).length === 0 && (selectedItem = this.element);
                        var addChildNode;
                        var childNode = $(this._cutCopyNodeText).clone();
                        if ($(childNode.find('a')[0]).hasClass('e-active'))
                            $(childNode.find('a')[0]).removeClass('e-active');
                        if ($(childNode).hasClass('first'))
                            addChildNode = $(childNode).removeClass('first').addClass('last');
                        else if (!$(childNode).hasClass('last'))
                            addChildNode = $(childNode).addClass('last');
                        else
                            addChildNode = $(childNode);

                        if (this.model.showCheckbox) {
                            childNode.find("span.e-chkbox-wrap").ejCheckBox({ "check": false, change: this._checkedChange });
                        }
                        // wire mouse over event 
                        this._on(childNode.find(".e-text"), "mouseenter", this._mouseEnterEvent);
                        this._on(childNode.find(".e-text"), "mouseleave", this._mouseLeaveEvent);

                        if ($(selectedItem).children('div').children('span').hasClass('child'))
                            $(selectedItem).children('div').children('span').removeClass('child').addClass('expandimg');
                        if ($(selectedItem).children('ul').length != 0) {
                            $(selectedItem).children('ul').find('>li').last().removeClass('last');
                            $(selectedItem).find('>ul').append($(addChildNode));
                        }
                        else {
                            var wrapUl = ej.buildTag('ul.e-treeview-ul');
                            node = $(wrapUl).append($(addChildNode));
                            $(selectedItem).append($(node));
                            $(selectedItem).children('div').find('>div').first().addClass('e-icon e-minus');
                        }
                        var liCollection = $(selectedItem).parents('li.e-item');
                        liCollection.each(function (index) {
                            var licoll = $(liCollection[index]).children('ul').find('>li');
                            if (licoll.length == 1) {
                                $(licoll).addClass('last');
                            }
                        });
                        this._expandNode(selectedItem);
                    }
                }
                    //Home Key(Select the first node)
                else if (code == 36 && !e.ctrlKey && !e.altKey && !e.shiftKey) {

                    $($(selectedItem).find('a')[0]).removeClass('e-active');
                    nextElement = $($(element).find('>li')[0]).find('a')[0];
                    $(nextElement).addClass('e-active');
                    proxy._KeyPressEventHandler(nextElement, proxy, code);
                    e.preventDefault();
                }

                    //End Key(Select the last node)
                else if (code == 35 && !e.ctrlKey && !e.altKey && !e.shiftKey) {

                    $($(selectedItem).find('a')[0]).removeClass('e-active');
                    node = $(element).find('>li').last();
                    if ($(node).find('div').hasClass('e-minus')) {
                        nextElement = $($(node).find('li').last().find('a')[0]);
                        $(nextElement).addClass('e-active');
                    }
                    else {
                        nextElement = $(node.find('a')[0]);
                        $(nextElement).addClass('e-active');
                    }
                    proxy._KeyPressEventHandler(nextElement, proxy, code);
                    e.preventDefault();
                }

            }
        },
        //Remove the node
        _removeNode: function (node) {
            var selectedNode = ej.isNullOrUndefined(node) ? this.element.find('.e-active') : node;
            if (selectedNode != null) {
                var parentNode = $($(selectedNode).parents('li.e-item')[1]);
                $($(selectedNode).parents('li.e-item')[0]).remove();
                this._modifiCss(parentNode);
            }
        },
        _KeyPressEventHandler: function (nextElement, proxy, code) {
            var isExpanded;
            var element = $($(nextElement).parents('li.e-item')[0]);
            var path = proxy._getPath($(nextElement).parents('li.e-item')[0]);
            var nodeText = $(nextElement).text();
            if ($($(nextElement).parents('li.e-item')[0]).children('div').find('div').hasClass('e-minus'))
                isExpanded = true;
            else
                isExpanded = false;
            var argsData = { keyCode: code, currentElement: element, value: nodeText, isExpanded: isExpanded, path: path };
            this._trigger("keyPress", argsData);
        },

        //Document click event handler
        _documentClick: function (event) {
            if (event.target.id != 'Edit_Input')
                var editTextBox = $('#Edit_Input')[0];
            if (editTextBox != null) {
                $(editTextBox).next().html(editTextBox.value).removeAttr('style');
                $(editTextBox).remove();
                //this.element.focus();
            }
        },
        //Save Changes on the edited node
        _saveAction: function (values) {
            $(values).next().html(values.value).removeAttr('style');
            $(values).remove();
        },

        //Cancels the changes 
        _cancelAction: function (values) {
            $(values).next().html(this.beforeEditText).removeClass('e-node-hover').addClass('e-active').removeAttr('style');
            this._hideEditTextBox();
        },

        //Hides the edit textbox
        _hideEditTextBox: function () {
            var editTextBox = $('#Edit_Input')[0];
            if (editTextBox != null) {
                $(editTextBox).remove();
            }
        },

        _mouseEnterEvent: function (event) {
            if ($(event.target).hasClass("e-text") && !$(event.target).hasClass("e-node-disable"))
                $(event.target).addClass("e-node-hover");
        },
        _mouseLeaveEvent: function (event) {
            if ($(event.target).hasClass("e-node-hover"))
                $(event.target).removeClass("e-node-hover");
        },

        _createSubNodesWhenLoadOnDemand: function (jsonData, element, mapper) {
            ej.isNullOrUndefined(mapper) && (mapper = this.model.fields);
            var customElement, imgSpan;
            var context = this;
            var idField = mapper.id;
            var textField = mapper.text;
            var parentIdField = mapper.parentId;
            var childField = mapper.childItems;
            var hasChildField = mapper.hasChild;
            var imgField = mapper.imageUrl;
            var checkedField = mapper.isChecked;
            var spriteCssClass = mapper.spriteCssClass;
            var outerMainUl = ej.buildTag("ul.e-treeview-ul", "", { display: "none" });
            $.each(jsonData, function (i, val) {
                var textElement = ej.buildTag("a.e-text CanSelect", val[textField], "", { href: "#" + val[textField] });
                context._on($(textElement), "mouseenter", context._mouseEnterEvent);
                context._on($(textElement), "mouseleave", context._mouseLeaveEvent);

                // Element to show expand, collapse image
                var exCollpasediv = ej.buildTag('div');
                //Span to show the images for Themes
                var span = ej.buildTag('span');
                if (val[childField] || val[hasChildField]) {
                    $(span).addClass('collapseimg');
                    $(exCollpasediv).addClass("e-icon e-plus");
                }
                else {
                    $(span).addClass('child');
                }

                //To add image
                if (val[imgField] != null) {
                    var imgDiv = ej.buildTag('img.e-align');
                    imgDiv.src = (val[imgField].substr(0, 1) == "~") ? val[imgField].substr(1, val[imgField].length) : val[imgField];
                }

                if (context.model.showCheckbox) {
                    //Create a checkbox
                    var index = $($(element).parents('li.e-item')[0]).find('div').find('>input[type=hidden]')[0].value;

                    var checkbox = ej.buildTag("input.nodecheckbox", "", {}, { type: "checkbox", name: "TreeView_Checkbox" + index + '_' + i + ".Checked" });

                    //Create a hidden Checkbox
                    var checkboxhdn = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox.Index", value: index + '_' + i });

                    if (val[checkedField] != null && val[checkedField])
                        $(checkbox).prop("checked", true);
                    $(checkbox).prop("value", val[checkedField] != null ? "False" : val[checkedField]);
                }
                //Outter div element 
                var outerdiv = ej.buildTag('div');
                $(outerdiv).append(exCollpasediv);
                if (context.model.showCheckbox) {
                    $(outerdiv).append(checkbox);
                    $(outerdiv).append(checkboxhdn);
                }
                $(outerdiv).append(span);
                if (val[spriteCssClass] != null) {
                    var imgSpan = ej.buildTag('spane.' + val[spriteCssClass]);
                    $(outerdiv).append(imgSpan);
                }
                $(outerdiv).append(imgDiv);
                $(outerdiv).append(customElement);
                $(outerdiv).append(textElement);


                //Check for sub-tree view items and append them
                var outerli = ej.buildTag('li.e-item', "", "", { id: val[idField] });
                if (context.model.allowEdit)
                    outerli.addClass('AllowEdit');
                if (context.model.showCheckbox && val[checkedField] != null && val[checkedField])
                    outerli.addClass("ischecked");
                if (jsonData.length == (i + 1))
                    outerli.addClass('last');
                outerli.append($(outerdiv));

                var liHasul = $(outerli);
                if (val["ChildCount"] > 0) {
                    $($(liHasul).children().find('div')[0]).removeClass('e-icon e-minus').addClass('e-icon e-plus');
                }
                $(outerMainUl).append($(outerli));
            });
            this.element.find(" li#" + jsonData[0][parentIdField] + " ul ").remove();
            this.element.find(" li#" + jsonData[0][parentIdField] + "").append($(outerMainUl));
            var parentLi=this.element.find(" li#" + jsonData[0][parentIdField]);
            if($(parentLi.find(".nodecheckbox[type=checkbox]")).prop('checked'))
				this._nodeCheckedAction($(parentLi).find("span.e-chkbox-wrap").first());
            //var licoll = $(outerMainUl).find('li');
            this.model.showCheckbox && $(outerMainUl).find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
            $(element).removeClass("e-load");
            this._isCheckedAction();
            //this._expandCollapseAction(element);
            //this.waitingPopup.HidePopUp();
        },

        _createTreeElement: function (nodeText, selectedNode, nodeUrl) {
            var value;
            if (this.model.showCheckbox) {
                if (!ej.isNullOrUndefined(selectedNode)) {
                    if ($(selectedNode.parents('li.e-item')[0]).children('ul').length == 0)
                        value = $($(selectedNode.parents('li.e-item')[0]).find('div')[0]).find('>input[type=hidden]')[0].value + '.' + 0;
                    else {
                        var prevlivalue = $($(selectedNode.parents('li.e-item')[0]).children('ul').find('li').last().find('div').find('>input[type=hidden]')[0]).val();
                        var splitvalue = prevlivalue.split('.');
                        var indexvalue = parseInt(splitvalue[splitvalue.length - 1]) + 1;
                        var previndexvalue = prevlivalue.substring(0, (prevlivalue.length) - 1);
                        value = previndexvalue + indexvalue;
                    }
                }
                else {
                    var list = this.element.find(">li");
                    value = list.length + 1;
                }

                var checkbox = ej.buildTag("input.nodecheckbox", "", {}, { type: "checkbox", name: "TreeView_Checkbox" + value + ".Checked", value: "False" });

                //Create a hidden Checkbox
                var checkboxhdn = ej.buildTag("input", "", {}, { type: "hidden", name: "TreeView_Checkbox.Index", value: value });

            }
            var textElement = ej.buildTag('a.e-text CanSelect', nodeText, "", { href: "#" + nodeText });
            if (!ej.isNullOrUndefined(nodeUrl))
                textElement.attr("href", nodeUrl);
            var textNode = $(textElement);
            this._on(textNode, "mouseenter", this._mouseEnterEvent);
            this._on(textNode, "mouseleave", this._mouseLeaveEvent);
            //Span to show the images for Themes
            var span = ej.buildTag('span.child');

            // Element to show expand, collapse image
            var exCollpasediv = ej.buildTag('div');
            //Outter div element 
            var outerdiv = ej.buildTag('div');
            $(outerdiv).append(exCollpasediv);
            $(outerdiv).append(span);
            if (this.model.showCheckbox) {
                $(outerdiv).append(checkbox);
                $(outerdiv).append(checkboxhdn);
            }
            $(outerdiv).append(textElement);

            //Check for sub-tree view items and append them
            var outerli = ej.buildTag('li.e-item#' + nodeText);
            if (this.model.allowEdit)
                outerli.addClass('AllowEdit');
            outerli.append($(outerdiv));

            return $(outerli);
        },
        _addExpandedNodes: function (index) {
            var _nodes = this.model.expandedNodes;
            if ($.inArray(index, _nodes) == -1) {
                this.model.expandedNodes.push(index);
            }
        },

        _removeExpandedNodes: function (index) {
            var _nodes = this.model.expandedNodes;
            if ($.inArray(index, _nodes) > -1) {
                this.model.expandedNodes.splice($.inArray(index, _nodes), 1);
            }
        },

        //-------------------- Event Wire-up -------------------------//
        _wireEvents: function () {
            this._on(this.element, "click", this._ClickEventHandler);
            this._on(this.element.find(".e-text"), "mouseenter", this._mouseEnterEvent);
            this._on(this.element.find(".e-text"), "mouseleave", this._mouseLeaveEvent);
            if (this.model.allowEdit) {
                this._on($(document), 'click', this._documentClick);
                this._on(this.element.parent(), 'dblclick', this._inlineEdit);
            }
            this._on(this.element, this.model.expandEvent, this._expandEventHandler);
            this._on($(document), 'keydown', this._KeyPress);
            this._on(this.element, "focus", this._onFocusHandler);
            this._enableDargDrop();
        },

        //-------------------- Event UnWire-up -------------------------//
        _unWireEvents: function () {
            this._off(this.element, "click");
            this._off(this.element.find(".e-text"), "mouseenter");
            this._off(this.element.find(".e-text"), "mouseleave");
            this._off($(document), 'click');
            this._off(this.element.parent(), 'dblclick');
            this._off(this.element, this.model.expandEvent);
            this._off($(document), 'keydown');
            this._on(this.element, "focus");
        },

        //Darg and Drop Events

        _enableDargDrop: function () {
            if (this.model.dragAndDrop) {
                this._drag();
                if (this.model.dropChild)
                    this._childDrop();
                if (this.model.dropSibling)
                    this._siblingDrop();
            }
        },
        //--------------------- Client side Methods ----------------------//
        refresh: function () {
            this._unWireEvents();
            this._init();
        },
        expandAll: function () {
            this.model.enabled && this._expandAll();
        },

        collapseAll: function () {
            this.model.enabled && this._collapseAll();
        },
        checkAll: function () {
            if (this.model.showCheckbox && this.model.enabled) {
                this._checkAll();
            }
        },
        unCheckAll: function () {
            if (this.model.showCheckbox && this.model.enabled) {
                this._unCheckAll();
            }
        },
        //Get Selected node
        getSelectedNode: function () {
            var selectedNodeElement = this.element.find('.e-active');
            var selectedNode = $($(selectedNodeElement).parents('li.e-item')[0]);
            return selectedNode;
        },

        getText: function (node) {
            if (ej.isNullOrUndefined(node)) return false;
            var nodeText = $($(node).find('a')[0]).text();
            return nodeText;
        },

        //Select the node
        selectNode: function (node) {
            var element;
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                if ($(node).hasClass("e-text")) {
                    element = $(node);
                }
                else
                    element = $($(node).find(">div >a")[0]);
                this._nodeSelectionAction(element);
            }
        },

        //unselected node
        unSelectNode: function (node) {
            var element;
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                if ($(node).hasClass("e-text")) {
                    element = $(node);
                }
                else
                    element = $($(node).find(">div >a")[0]);
                this._nodeUnSelectionAction(element);
            }
        },

        //Enable the node
        enableNode: function (node) {
            var element;
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                if ($(node).hasClass("e-text")) {
                    element = $(node);
                }
                else
                    element = $($(node).find("a.e-text")[0]);
                this._nodeEnableAction(element);
                $(node).parent().find(".nodecheckbox").ejCheckBox("enable");
            }
        },

        //Disable node
        disableNode: function (node) {
            var element;
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                if ($(node).hasClass("e-text")) {
                    element = $(node);
                }
                else
                    element = $($(node).find(">div >a")[0]);
                this._nodeDisableAction(element);
                $(node).find(".nodecheckbox").ejCheckBox("disable");
            }
        },

        addNodeCollection: function (collection, targetNode) {
            if (collection && collection.length > 0) {
                var mapper = this.model.fields;

                for (var i = 0; i < collection.length; i++) {
                    var node = collection[i], text = node[mapper.text];

                    if (text) {
                        var id = node[mapper.id], pid = node[mapper.parentId],
                        url = node[mapper.linkAttribute] ? node[mapper.linkAttribute] : "#",
                        parent = (targetNode && targetNode.length != 0) ? targetNode : null;

                        if (ej.isNullOrUndefined(parent)) {
                            var tar = pid ? this.element.find("#" + pid) : null;
                            parent = (tar && tar.length != 0) ? tar.children("div").find("a") : null;
                        }
                        if (ej.isNullOrUndefined(parent)) continue;

                        var nodeElement = this._createTreeElement(text, null, url);
                        id ? nodeElement.attr("id", id) : nodeElement.removeAttr("id");
                        this._appendNode(parent, nodeElement);
                        nodeElement.find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
                        if (this.model.dragAndDrop) nodeElement.find("a").addClass("e-draggable e-droppable");
                    }
                }
            }
        },

        //Add the node
        addNode: function (newNodeText, nodeUrl, targetNode) {
            if (this.model.enabled) {
                var outerLi = null;
                var innerUl = null;
                var element = this.element;
                var childCount = element.children().length;
                var selectedNode = ej.isNullOrUndefined(targetNode) ? element.find('.e-active') : targetNode;
                if (selectedNode.length == 0) {
                    if (childCount == 0 || element.children().children().length == 0)
                        innerUl = ej.buildTag('ul');
                    else
                        innerUl = element.children()[childCount - 1];
                    outerLi = this._createTreeElement(newNodeText, null, nodeUrl);
                    $(innerUl).append(outerLi);
                    this._appendNode(selectedNode, innerUl);
                    $(outerLi).find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
                }
                else {
                    this._expandNode($($(selectedNode).parents('li.e-item')[0]));
                    outerLi = this._createTreeElement(newNodeText, selectedNode, nodeUrl);
                    this._appendNode(selectedNode, outerLi);
                    this.model.showCheckbox && $(outerLi).find(".nodecheckbox").ejCheckBox({ cssClass: this.model.cssClass, change: this._checkedChange });
                }
                this._modifiCss();
            }
        },

        //Remove the node
        removeNode: function (node) {
            if (this.model.enabled) {
                if (!ej.isNullOrUndefined(node)) {
                    if ($(node).hasClass("e-text")) {
                        element = $(node);
                    }
                    else
                        element = $($(node).find(">div >a")[0]);
                }
                this._removeNode(element);
            }
        },

        //Get all the checked  nodes
        getAllCheckedNodes: function () {
            var nodeCollection = this.element.find('input.nodecheckbox[value="True"]').closest('li');
            return nodeCollection;
        },
        //Check particular node checkbox
        checkNode: function (node) {
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                this._nodeCheckedAction($(node).find("span.e-chkbox-wrap").first());
            }
        },

        //UnCheck particular node textbox
        unCheckNode: function (node) {
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                this._nodeUnCheckedAction($(node).find("span.e-chkbox-wrap").first());
            }
        },
        expandNode: function (node) {
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                this._expandNode(node);
            }
        },

        //Collapses the selected node
        collapseNode: function (node) {
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                this._collpaseNode(node);
            }
        },
        showNode: function (node) {
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                node.css("visibility", "");
            }
        },
        hideNode: function (node) {
            if (!ej.isNullOrUndefined(node) && this.model.enabled) {
                node.css("visibility", "hidden");
            }
        },
        show: function () {
            /// <summary>To Show the TreeView control</summary>
            this.element.css("visibility", "");
        },
        hide: function () {
            /// <summary>To Hide the TreeView control</summary>
            this.element.css("visibility", "hidden");
        },
        isNodeChecked: function (element) {
            return this._isChecked($(element).find('input.nodecheckbox')[0]);
        },
        isExpanded: function (element) {
            return this._isExpanded(element);
        },

        isNodeExpanded: function (element) {
            if (!ej.isNullOrUndefined(element)) {
                var expandUl = $(element).children().filter('ul');
                var isExpanded = $(expandUl).is(':not(:hidden)');
                return isExpanded;
            }
        },

        hasChildNode: function (element) {
            var child = false;
            if (!ej.isNullOrUndefined(element)) {
                child = $(element).children().length < 2 ? false : true;
            }
            return child;
        },
        //--------------------- Client side events ----------------------//

        _onNodeExpand: function (data) {
            if (this.model.expand) {
                this._trigger("expand", data);
            }
        },
        _onNodeCollapse: function (data) {
            if (this.model.collapse) {
                this._trigger("collapse", data);
            }
        },
        _onBeforeExpand: function (data) {
            if (this.model.beforeExpand) {
                return this._trigger("beforeExpand", data);
            }
        },
        _onBeforeCollapse: function (data) {
            if (this.model.beforeCollapse) {
                return this._trigger("beforeCollapse", data);
            }
        },
        _onNodeSelect: function (data) {
            if (this.model.select) {
                this._trigger("select", data);
            }
        },
        _onChecked: function (element) {
            var liElement = $(element).parents("li").first();
            var nodeText = liElement.find(".e-text").first().text();
            var isChecked = this._isChecked(element);
            var data = { currentElement: liElement, value: nodeText, checked: isChecked };
            if (this.model.check) {
                this._trigger("check", data);
            }
        },
        _onUnChecked: function (element) {
            var liElement = $(element).parents("li").first();
            var nodeText = liElement.find(".e-text").first().text();
            var isUnChecked = this._isChecked(element);
            var data = { currentElement: liElement, value: nodeText, checked: isUnChecked };
            if (this.model.uncheck) {
                this._trigger("uncheck", data);
            }
        },
        _onDragStarts: function (data) {
            if (this.model.dragStart) {
                return this._trigger("dragStart", data);
            }
        },
        _onDrag: function (data) {
            if (this.model.drag) {
                return this._trigger("drag", data);
            }
        },
        _onDragStop: function (data) {
            if (this.model.dragStop) {
                return this._trigger("dragStop", data);
            }
        },
        _onDropped: function (data) {
            if (this.model.dropped) {
                return this._trigger("dropped", data);
            }
        },
        _onClick: function (data) {
            this._trigger("click", data);
        }
    });

})(jQuery, Syncfusion);