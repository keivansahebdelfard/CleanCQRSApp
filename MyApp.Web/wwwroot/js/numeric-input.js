(function () {
    'use strict';

    function bindNumericInput(el) {
        if (!el || el.dataset.numericInit === '1') return;
        el.dataset.numericInit = '1';

        // Prevent typing e, E, +, -, . and commas
        el.addEventListener('keydown', function (e) {
            debugger
            var blocked = ['e', 'E', '+', '-', '.', ','];
            if (blocked.indexOf(e.key) !== -1) e.preventDefault();
        });

        // Sanitize input to digits only (handles paste and other inputs)
        el.addEventListener('input', function () {
            var cleaned = this.value.replace(/[^0-9]/g, '');
            if (this.value !== cleaned) this.value = cleaned;
        });

        // Sanitize paste
        el.addEventListener('paste', function (e) {
            e.preventDefault();
            var text = (e.clipboardData || window.clipboardData).getData('text');
            var digits = text.replace(/[^0-9]/g, '');
            var start = this.selectionStart;
            var end = this.selectionEnd;
            var val = this.value;
            this.value = val.slice(0, start) + digits + val.slice(end);
            // move cursor
            var pos = start + digits.length;
            this.setSelectionRange(pos, pos);
        });
    }

    function initNumericInputs(root) {
        root = root || document;
        var inputs = root.querySelectorAll('.price');
        inputs.forEach(function (price) {
            bindNumericInput(price);
        });
    }

    // Initialize on DOMContentLoaded
    document.addEventListener('DOMContentLoaded', function () {
        initNumericInputs(document);

        // MutationObserver to auto-initialize dynamically added inputs or when class is added
        var observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (m) {
                if (m.type === 'attributes' && m.attributeName === 'class') {
                    var target = m.target;
                    if (target && target.classList && target.classList.contains('price')) {
                        bindNumericInput(target);
                    }
                }

                if (m.addedNodes && m.addedNodes.length) {
                    m.addedNodes.forEach(function (node) {
                        if (!node.querySelectorAll) return;
                        var newInputs = node.querySelectorAll('.price');
                        newInputs.forEach(function (ni) { bindNumericInput(ni); });
                        // If the node itself has the class
                        if (node.classList && node.classList.contains('price')) bindNumericInput(node);
                    });
                }
            });
        });

        observer.observe(document.body, { childList: true, subtree: true, attributes: true, attributeFilter: ['class'] });
    });

    // Also expose in case of dynamic content
    window.NumericInput = {
        init: initNumericInputs,
        bind: bindNumericInput
    };
})();