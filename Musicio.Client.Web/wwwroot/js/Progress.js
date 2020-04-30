window.get_offsetWidth = (id) => {
    return document.getElementById(id).offsetWidth;
};

window.get_offsetLeft = (id) => {
    return document.getElementById(id).offsetLeft;
};

window.functions = () => { };

window.functions.setTimeout = (time) => {
    setTimeout(function () {
        DotNet.invokeMethod('Musicio.Client.Web', 'ExecuteTimeoutFunc');
    }, time);
};