window.ej = window.Syncfusion = window.Syncfusion || {};

(function ($, ej, undefined) {
    'use strict';

    ej.consts = {
        NamespaceJoin: '-'
    };
    ej.textAlign = {
        Center: 'center',
        Justify: 'justify',
        Left: 'left',
        Right: 'right'
    };
    ej.orientation = { Horizontal: "horizontal", Vertical: "vertical" };

    if (!Object.prototype.hasOwnProperty) {
        Object.prototype.hasOwnProperty = function (obj, prop) {
            return obj[prop] !== undefined;
        };
    }

    String.format = function () {
        var source = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++)
            source = source.replace(new RegExp("\\{" + i + "\\}", "gm"), arguments[i + 1]);

        source = source.replace(/\{[0-9]\}/g, "");
        return source;
    };

    // Function to create new class
    ej.defineClass = function (className, constructor, proto, replace) {
        /// <summary>Creates the javascript class with given namespace & class name & constructor etc</summary>
        /// <param name="className" type="String">class name prefixed with namespace</param>
        /// <param name="constructor" type="Function">constructor function</param>
        /// <param name="proto" type="Object">prototype for the class</param>
        /// <param name="replace" type="Boolean">[Optional]Replace existing class if exists</param>
        /// <returns type="Function">returns the class function</returns>
        if (!className || !proto) return undefined;

        var parts = className.split(".");

        // Object creation
        var obj = window, i = 0;
        for (; i < parts.length - 1; i++) {

            if (ej.isNullOrUndefined(obj[parts[i]]))
                obj[parts[i]] = {};

            obj = obj[parts[i]];
        }

        if (replace || ej.isNullOrUndefined(obj[parts[i]])) {

            //constructor
            constructor = typeof constructor === "function" ? constructor : function () {
            };

            obj[parts[i]] = constructor;

            // prototype
            obj[parts[i]].prototype = proto;
        }

        return obj[parts[i]];
    };

    ej.util = {
        getNameSpace: function (className) {
            /// <summary>Internal function, this will create namespace for plugins using class name</summary>
            /// <param name="className" type="String"></param>
            /// <returns type="String"></returns>
            var splits = className.toLowerCase().split(".");
            splits[0] === "ej" && (splits[0] = "e");

            return splits.join(ej.consts.NamespaceJoin);
        },

        getObject: function (nameSpace, from) {
            if (!from) return undefined;

            var value = from, splits = nameSpace.split('.');

            for (var i = 0; i < splits.length; i++) {

                if (ej.util.isNullOrUndefined(value)) break;

                value = value[splits[i]];
            }

            return value;
        },

        createObject: function (nameSpace, value, initIn) {
            var splits = nameSpace.split('.'), start = initIn || window, from = start, i;

            for (i = 0; i < splits.length; i++) {

                if (i + 1 == splits.length)
                    from[splits[i]] = ej.isNullOrUndefined(value) ? {} : value;
                else if (ej.isNullOrUndefined(from[splits[i]]))
                    from[splits[i]] = {};

                from = from[splits[i]];
            }

            return start;
        },

        isNullOrUndefined: function (value) {
            /// <summary>Util to check null or undefined</summary>
            /// <param name="value" type="Object"></param>
            /// <returns type="Boolean"></returns>
            return value === undefined || value === null;
        },

        buildTag: function (tag, innerHtml, styles, attrs) {
            /// <summary>Helper to build jQuery element</summary>
            /// <param name="tag" type="String">tagName#id.cssClass</param>
            /// <param name="innerHtml" type="String"></param>
            /// <param name="styles" type="Object">A set of key/value pairs that configure styles</param>
            /// <param name="attrs" type="Object">A set of key/value pairs that configure attributes</param>
            /// <returns type="jQuery"></returns>
            var tagName = /^[a-z]*[0-9a-z]+/ig.exec(tag)[0];

            var id = /#([a-z]+[-_0-9a-z]+)/ig.exec(tag);
            id = id ? id[id.length - 1] : undefined;

            var className = /\.([a-z]+[-_0-9a-z ]+)/ig.exec(tag);
            className = className ? className[className.length - 1] : undefined;

            return $(document.createElement(tagName))
                .attr(id ? { "id": id } : {})
                .addClass(className || "")
                .css(styles || {})
                .attr(attrs || {})
                .html(innerHtml || "");
        }
    };

    $.extend(ej, ej.util);

    // base class for all ej widgets. It will automatically inhertied
    ej.widgetBase = {
        droppables: { 'default': [] },
        resizables: { 'default': [] },

        destroy: function () {

            if (this._trigger("destroy"))
                return;

            if (this.model.persist)
                this.persistState();

            this._destroy();

            var arr = this.element.data("ejWidgets") || [];
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] == this.pluginName) {
                    arr.splice(i, 1);
                }
            }
            if (!arr.length)
                this.element.removeData("ejWidgets");

            while (this._events) {
                var item = this._events.pop(), args = [];

                if (!item)
                    break;

                for (var i = 0; i < item[1].length; i++)
                    if (!$.isPlainObject(item[1][i]))
                        args.push(item[1][i]);

                $.fn.off.apply(item[0], args);
            }

            this._events = null;

            this.element
                .removeClass(ej.util.getNameSpace(this.sfType))
                .removeClass("e-js")
                .removeData(this.pluginName);

            this.element = null;
            this.model = null;
        },

        _on: function (element) {
            if (!this._events)
                this._events = [];
            var args = [].splice.call(arguments, 1, arguments.length - 1);

            var handler = {}, i = args.length;
            while (handler && typeof handler !== "function") {
                handler = args[--i];
            }

            args[i] = ej.proxy(args[i], this);

            this._events.push([element, args, handler, args[i]]);

            $.fn.on.apply(element, args);

            return this;
        },

        _off: function (element, eventName, selector, handlerObject) {
            var e = this._events,temp;
            if (!e || !e.length)
                return this;
            if (typeof selector == "function") {
                temp = handlerObject;
                handlerObject = selector;
                selector = temp;
            }
            for (var i = 0; i < e.length; i++) {
                var arg = e[i],
                r = arg[0].length &&(!handlerObject || arg[2] === handlerObject) && arg[1][0] === eventName && (!selector || arg[1][1] === selector) && $.inArray(element[0], arg[0]) > -1;
                if (r) {
                    $.fn.off.apply(element, handlerObject ? [eventName, selector, arg[3]] : [eventName, selector]);
                    e.splice(i, 1);
                    break;
                }
            }

            return this;
        },

        // Client side events wire-up / trigger helper.
        _trigger: function (eventName, eventProp) {
            var fn = this.model[eventName], returnValue, args;

            if (fn) {
                if (typeof fn === "string") {
                    fn = ej.util.getObject(fn, window);
                }

                if ($.isFunction(fn)) {

                    args = ej.event(eventName, this.model, eventProp);

                    returnValue = fn.call(this, args);

                    // sending changes back - deep copy option should not be enabled for this $.extend 
                    if (eventProp) $.extend(eventProp, args);

                    if (args.cancel || !ej.isNullOrUndefined(returnValue))
                        return returnValue === false || args.cancel;
                }
            }

            var isPropDefined = Boolean(eventProp);
            eventProp = eventProp || {};
            eventProp.originalEventType = eventName;
            eventProp.type = this.pluginName + eventName;

            args = $.Event(eventProp.type, ej.event(eventProp.type, this.model, eventProp));

            this.element.trigger(args);

            // sending changes back - deep copy option should not be enabled for this $.extend 
            if (isPropDefined) $.extend(eventProp, args);

            return args.cancel;
        },

        setModel: function (options, forceSet) {
            // check for whether to apply values are not. if _setModel function is defined in child,
            //  this will call that function and validate it using return value

            if (this._trigger("modelChange", { "changes": options }))
                return;


            for (var prop in options) {
                if (!forceSet && this.model[prop] === options[prop]) {
                    delete options[prop];
                    continue;
                }

                if (this.dataTypes) {
                    var returnValue = this._isValidModelValue(prop, this.dataTypes, options);
                    if (returnValue !== true)
                        throw "setModel - Invalid input for property :" + prop + " - " + returnValue;
                }
                if (this.model.notifyOnEachPropertyChanges && this.model[prop] !== options[prop]) {
                    var arg = {
                        oldValue: this.model[prop],
                        newValue: options[prop]
                    };

                    options[prop] = this._trigger(prop + "Change", arg) ? this.model[prop] : arg.newValue;
                }
            }
            if ($.isEmptyObject(options))
                return;

            if (this._setFirst) {
                var ds = options.dataSource;
                if (ds) delete options.dataSource

                $.extend(true, this.model, options);
                if (ds) {
                    var tempds = [];
                    this.model.dataSource = $.extend(true, tempds, ds);
                    options["dataSource"] = tempds;
                }
                !this._setModel || this._setModel(options);

            } else if (!this._setModel || this._setModel(options) !== false) {
                $.extend(true, this.model, options);
            }
            if ("persist" in options) {
                if (options.persist === true) {
                    this._persistHandler = ej.proxy(this.persistState, this);
                    $(window).on("unload", this._persistHandler);
                } else {
                    $(window).off("unload", this._persistHandler);
                }
            }
        },

        option: function (prop, value, forceSet) {
            if (!prop)
                return this.model;

            if ($.isPlainObject(prop))
                return this.setModel(prop);

            if (typeof prop === "string") {
                prop = prop.replace(/^model\./, "");
                var oldValue = ej.getObject(prop, this.model);

                if (value === undefined)
                    return oldValue;

                if (forceSet && value === ej.extensions.modelGUID) {
                    return this._setModel(ej.createObject(prop, ej.getObject(prop, this.model), {}));
                }

                if (forceSet || ej.getObject(prop, this.model) !== value)
                    return this.setModel(ej.createObject(prop, value, {}), forceSet);
            }
            return undefined;
        },

        _isValidModelValue: function (prop, types, options) {
            var value = types[prop], option = options[prop], returnValue;

            if (!value)
                return true;

            if (typeof value === "string") {
                if (value == "enum") {
                    options[prop] = option ? option.toString().toLowerCase() : option;
                    value = "string";
                }

                if(value === "array"){
                    if(option instanceof Array)
                        return true;
                }
                else if (value === "data") {
                    return true;
                }
                else if (typeof option === value)
                    return true;

                return "Expected type - " + value;
            }

            if (option instanceof Array) {
                for (var i = 0; i < option.length; i++) {
                    returnValue = this._isValidModelValue(prop, types, option[i]);
                    if (returnValue !== true) {
                        return " [" + i + "] - " + returnValue;
                    }
                }
                return true;
            }

            for (var innerProp in option) {
                returnValue = this._isValidModelValue(innerProp, value, option);
                if (returnValue !== true)
                    return innerProp + " : " + returnValue;
            }

            return true;
        },

        persistState: function () {
            var model = copyObject({}, this.model);

            if (this.ignoreOnPersist) {
                for (var i = 0; i < this.ignoreOnPersist; i++) {
                    delete model[this.ignoreOnPersist[i]];
                }
            }

            if (this._persistState) {
                model.customPersists = {};
                this._persistState(model.customPersists);
            }

            if (window.localStorage)
                window.localStorage.setItem(this.pluginName + this._id, JSON.stringify(model));
            else if (document.cookie)
                ej.cookie.set(this.pluginName + this._id, model);
        },

        restoreState: function (silent) {
            var value = null;
            if (window.localStorage)
                value = window.localStorage.getItem(this.pluginName + this._id);
            else if (document.cookie)
                value = ej.cookie.get(this.pluginName + this._id);

            if (value) {
                var model = JSON.parse(value);

                if (this._restoreState) {
                    this._restoreState(model.customPersists);
                    delete model.customPersists;
                }

                this.model = $.extend(true, this.model, model);
            }

            if (!silent && value && this._setModel)
                this._setModel(this.model);
        }
    };

    ej.widget = function (pluginName, className, proto) {
        /// <summary>Widget helper for developers, this set have predefined function to jQuery plug-ins</summary>
        /// <param name="pluginName" type="String">the plugin name that will be added in jquery.fn</param>
        /// <param name="className" type="String">the class name for your plugin, this will help create default cssClas</param>
        /// <param name="proto" type="Object">prototype for of the plug-in</param>

        if (typeof pluginName === "object") {
            proto = className;
            for (var prop in pluginName) {
                var name = pluginName[prop];

                if (name instanceof Array) {
                    proto._rootCSS = name[1];
                    name = name[0];
                }

                ej.widget(prop, name, proto);

                if (pluginName[prop] instanceof Array)
                    proto._rootCSS = "";
            }

            return;
        }

        var nameSpace = proto._rootCSS || ej.getNameSpace(className);

        proto = ej.defineClass(className, function (element, options) {

            this.sfType = className;
            this.pluginName = pluginName;
            this.instance = pInstance;

            if(ej.isNullOrUndefined(this._setFirst))
                this._setFirst = true;

            this["ob.values"] = {};

            $.extend(this, ej.widgetBase);

            if (this.dataTypes) {
                for (var property in options) {
                    var returnValue = this._isValidModelValue(property, this.dataTypes, options);
                    if (returnValue !== true)
                        throw "setModel - Invalid input for property :" + property + " - " + returnValue;
                }
            }

            var arr = (element.data("ejWidgets") || []);
            arr.push(pluginName);
            element.data("ejWidgets", arr);

            for (var i = 0; ej.widget.observables && this.observables && i < this.observables.length; i++) {
                var t = ej.getObject(this.observables[i], options);
                if (t) ej.createObject(this.observables[i], ej.widget.observables.register(t, this.observables[i], this, element), options);
            }

            this.element = element.jquery ? element : $(element);
            this.model = copyObject({}, proto.prototype.defaults, options);
            this.model.keyConfigs = copyObject(this.keyConfigs);

            this._id = element[0].id;

            if (!this.element.attr("tabIndex"))
                this.element.attr("tabIndex", "");

            if (this.model.persist) {
                this._persistHandler = ej.proxy(this.persistState, this);
                $(window).on("unload", this._persistHandler);
                this.restoreState(true);
            }

            this._init();

            if (typeof this.model.keyConfigs === "object" && !(this.model.keyConfigs instanceof Array)) {
                var requiresEvt = false;
                if (this.model.keyConfigs.focus)
                    this.element.attr("accesskey", this.model.keyConfigs.focus);

                for (var keyProps in this.model.keyConfigs) {
                    if (keyProps !== "focus") {
                        requiresEvt = true;
                        break;
                    }
                }

                if (requiresEvt && this._keyPressed) {
                    var el = element, evt = "keydown";

                    if (this.keySettings) {
                        el = this.keySettings.getElement ? this.keySettings.getElement() || el : el;
                        evt = this.keySettings.event || evt;
                    }

                    this._on(el, evt, function (e) {
                        if (!this.model.keyConfigs) return;

                        var action = keyFn.getActionFromCode(this.model.keyConfigs, e.which, e.ctrlKey, e.shiftKey, e.altKey);
                        var arg = {
                            code: e.which,
                            ctrl: e.ctrlKey,
                            alt: e.altKey,
                            shift: e.shiftKey
                        };
                        if (!action) return;

                        if (this._keyPressed(action, e.target, arg, e) === false)
                            e.preventDefault();
                    });
                }
            }

            this._trigger("create");
        }, proto);

        $.fn[pluginName] = function (options) {

            for (var i = 0; i < this.length; i++) {

                var $this = $(this[i]),
                    pluginObj = $this.data(pluginName),
                    isAlreadyExists = pluginObj && $this.hasClass(nameSpace),
                    obj = null;

                // ----- plug-in creation/init
                if (!isAlreadyExists) {
                    if (!options || typeof options === "object") {
                        $this.addClass(nameSpace + " e-js").data(pluginName, new proto($this, options));
                    }
                    else {
                        throwError(pluginName + ": methods/properties can be accessed only after plugin creation");
                    }
                    continue;
                }

                if (!options) continue;

                // --- Function/property set/access
                if ($.isPlainObject(options)) {
                    // setModel using JSON object
                    pluginObj.setModel(options);
                }

                    // function/property name starts with "_" is private so ignore it.
                else if (options.indexOf('_') !== 0
                    && !ej.isNullOrUndefined(obj = ej.getObject(options, pluginObj))
                    || options.indexOf("model.") === 0) {

                    if (!obj || !$.isFunction(obj)) {

                        // if property is accessed, then break the jquery chain
                        if (arguments.length == 1)
                            return obj;

                        //setModel using string input
                        pluginObj.option(options, arguments[1]);

                        continue;
                    }

                    var value = obj.apply(pluginObj, [].splice.call(arguments, 1, arguments.length - 1));

                    // If function call returns any value, then break the jquery chain
                    if (!ej.util.isNullOrUndefined(value))
                        return value;

                } else {
                    throwError(className + ": function/property - " + options + " does not exist");
                }
            }

            // maintaining jquery chain
            return this;
        };

        ej.widget.register(pluginName, className, proto.prototype);
    };

    $.extend(ej.widget, (function () {
        var _widgets = {},

        register = function (pluginName, className, prototype) {
            if (!ej.isNullOrUndefined(_widgets[pluginName]))
                throwError("ej.widget : The widget named " + pluginName + " is trying to register twice.");

            _widgets[pluginName] = { name: pluginName, className: className, proto: prototype };
        }

        return {
            register: register,
            registeredWidgets: _widgets
        };

    })());

    ej.widget.destroyAll = function (elements) {
        if (!elements || !elements.length) return;

        for (var i = 0; i < elements.length; i++) {
            var data = elements.eq(i).data();
            if (data["ejWidgets"] && data["ejWidgets"].length) {
                for (var j = data["ejWidgets"].length - 1; j > -1; j--) {
                    if (data[data["ejWidgets"][j]] && data[data["ejWidgets"][j]].destroy)
                        data[data["ejWidgets"][j]].destroy();
                }
            }
        }
    };

    ej.cookie = {
        get: function (name) {
            var value = RegExp(name + "=([^;]+)").exec(document.cookie);

            if (value && value.length > 1)
                return value[1];

            return undefined;
        },
        set: function (name, value, expiryDate) {
            if (typeof value === "object")
                value = JSON.stringify(value);

            value = escape(value) + ((expiryDate == null) ? "" : "; expires=" + expiryDate.toUTCString());
            document.cookie = name + "=" + value;
        }
    };

    var keyFn = {
        getActionFromCode: function (keyConfigs, keyCode, isCtrl, isShift, isAlt) {
            isCtrl = isCtrl || false;
            isShift = isShift || false;
            isAlt = isAlt || false;

            for (var keys in keyConfigs) {
                if (keys === "focus") continue;

                var key = keyFn.getKeyObject(keyConfigs[keys]);
                if (keyCode === key.code && isCtrl == key.isCtrl && isShift == key.isShift && isAlt == key.isAlt)
                    return keys;
            }
            return null;
        },
        getKeyObject: function (key) {
            var res = {
                isCtrl: false,
                isShift: false,
                isAlt: false
            };
            key = key.split("+");
            for (var i = 0; i < key.length; i++) {
                if (key[i] === "ctrl")
                    res.isCtrl = true;
                else if (key[i] === "shift")
                    res.isShift = true;
                else if (key[i] === "alt")
                    res.isAlt = true;
                else res.code = parseInt(key[i], 10);
            }
            return res;
        }
    };

    ej.browserInfo = function () {
        var browser = {}, clientInfo = [],
        browserClients = {
            webkit: /(chrome)[ \/]([\w.]+)/i, safari: /(webkit)[ \/]([\w.]+)/i, msie: /(msie) ([\w.]+)/i,
            opera: /(opera)(?:.*version|)[ \/]([\w.]+)/i, mozilla: /(mozilla)(?:.*? rv:([\w.]+)|)/i
        };
        for (var client in browserClients) {
            if (browserClients.hasOwnProperty(client)) {
                clientInfo = navigator.userAgent.match(browserClients[client]);
                if (clientInfo) {
                    browser.name = clientInfo[1].toLowerCase();
                    browser.version = clientInfo[2];
                    if (!!navigator.userAgent.match(/Trident\/7\./)) {
                        browser.name = "msie";
                    }
                    break;
                }
            }
        }
        browser.isMSPointerEnabled = (browser.name == 'msie') && browser.version > 9 && window.navigator.msPointerEnabled;
        return browser;
    };

    ej.eventType = {
        mouseDown: "mousedown touchstart",
        mouseMove: "mousemove touchmove",
        mouseUp: "mouseup touchend",
        mouseLeave: "mouseleave touchcancel",
        click: "click touchend"
    };

    ej.event = function (type, data, eventProp) {

        var e = $.extend(eventProp || {},
            {
                "type": type,
                "model": data,
                "cancel": false
            });

        return e;
    };

    ej.proxy = function (fn, context, arg) {
        if (!fn || typeof fn !== "function")
            return null;

        if ('bind' in fn)
            return arg ? fn.bind(context, arg) : fn.bind(context);

        return function () {
            var args = arg ? [arg] : []; args.push.apply(args, arguments);
            fn.apply(context, args);
        };
    };

    ej.hasStyle = function (prop) {
        var style = document.documentElement.style;

        if (prop in style) return true;

        var prefixs = ['ms', 'Moz', 'Webkit', 'O', 'Khtml'];

        prop = prop[0].toUpperCase() + prop.slice(1);

        for (var i = 0; i < prefixs.length; i++) {
            if (prefixs[i] + prop in style)
                return true;
        }

        return false;
    };

    Array.prototype.indexOf = Array.prototype.indexOf || function (searchElement) {
        var len = this.length;

        if (len === 0) return -1;

        for (var i = 0; i < len; i++) {
            if (i in this && this[i] === searchElement)
                return i;
        }
        return -1;
    };

    String.prototype.startsWith = String.prototype.startsWith || function (key) {
        return this.slice(0, key.length) === key;
    };
    var copyObject = ej.copyObject = function (target) {
        var objects = [].splice.call(arguments, 1, arguments.length), current, source;

        for (var i = 0; i < objects.length; i++) {
            for (var prop in objects[i]) {
                current = target[prop], source = objects[i][prop];

                if (source === undefined || current === source || objects[i] === source || target === source)
                    continue;
                if (source instanceof Array) {
                    target[prop] = source.slice();
                } else if (ej.isPlainObject(source)) {
                    target[prop] = current || {};
                    copyObject(target[prop], source);
                } else
                    target[prop] = source;
            }
        }
        return target;
    };
    var pInstance = function () {
        return this;
    }
    ej.isPlainObject = function (obj) {
        if (!obj) return false;

        if (typeof obj !== "object" || obj.nodeType || jQuery.isWindow(obj)) {
            return false;
        }

        try {
            if (obj.constructor &&
                !obj.constructor.prototype.hasOwnProperty("isPrototypeOf")) {
                return false;
            }
        } catch (e) {
            return false;
        }

        var key, ownLast = ej.support.isOwnLast;
        for (key in obj) {
            if (ownLast) break;
        }

        return key === undefined || obj.hasOwnProperty(key);
    };
    ej.util.valueFunction = function (prop) {
        return function (value) {
            var val = ej.getObject(prop, this.model);

            if (value === undefined)
                return typeof val === "function" ? val() : val;

            if (typeof val === "function") {
                this["ob.values"][prop] = value;
                val.call(this, value);
            }
            else
                ej.createObject(prop, value, this.model);
        }
    };
    ej.util.getVal = function(val){
        if(typeof val === "function")
            return val();
        return val;
    };
    ej.support = {
        isOwnLast: function () {
            var fn = function () { this.a = 1; };
            fn.prototype.b = 1;

            for (var p in new fn()) {
                return p === "b";
            }
        }(),
        outerHTML: function () {
            return document.createElement("div").outerHTML !== undefined;
        }()
    };

    var throwError = ej.throwError = function (er) {
        try {
            throw new Error(er);
        } catch (e) {
            throw e.message + "\n" + e.stack;
        }
    };

    ej.extensions = {};
    ej.extensions.modelGUID = "{0B1051BA-1CCB-42C2-A3B5-635389B92A50}";
})(window.jQuery, window.Syncfusion);