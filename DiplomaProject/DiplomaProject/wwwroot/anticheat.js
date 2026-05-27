window.anticheat = {

    init: function (dotnetHelper) {

        document.addEventListener("visibilitychange", function () {
            if (document.hidden) {
                dotnetHelper.invokeMethodAsync("OnTabHidden");
            }
        });

        window.addEventListener("blur", function () {
            dotnetHelper.invokeMethodAsync("OnWindowBlur");
        });

        window.addEventListener("focus", function () {
            dotnetHelper.invokeMethodAsync("OnWindowFocus");
        });

        document.addEventListener("keyup", function (e) {
            if (e.key === "PrintScreen") {
                dotnetHelper.invokeMethodAsync("OnPrintScreen");
            }
        });
    },

    enableExitWarning: function () {
        window.addEventListener("beforeunload", function (e) {
            e.preventDefault();
            e.returnValue = '';
        });
    },

    blackout: function () {
        document.body.style.transition = "filter 0.2s ease";
        document.body.style.filter = "brightness(0)";
    },

    reset: function () {
        document.body.style.filter = "none";
    }
};