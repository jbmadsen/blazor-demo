document.onkeydown = function(evt) {
    evt = evt || window.event;
    if (evt.keyCode >= 48 && evt.keyCode <= 57 ) {
        evt.preventDefault();
        //alert("Key: ", evt.keyCode);
        DotNet.invokeMethod('Blazor.Client', 'KeyPressed', evt.keyCode);
              //.then(data => { /* add logic that operates on return data here */ });
    }
};