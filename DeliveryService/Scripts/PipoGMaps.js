function myMap() {
    var myCenter = new google.maps.LatLng(35.720776, 51.393462);
    var mapCanvas = document.getElementById("PipoMap");
    var mapOptions = { center: myCenter, zoom: 9 };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    var marker;
    google.maps.event.addListener(map, 'click', function (e) {
        var x = e.latLng.lat();
        var y = e.latLng.lng();
        var PipoCoordinate = document.getElementById("PipoCoordinate");
        PipoCoordinate.value = x + ',' + y;
        if (marker != null) {
            marker.setMap(null);
        }
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(x, y),
            map: map
        });
    });
}


function myMapEdit(x1,y1) {
    var myCenter = new google.maps.LatLng(x1,y1);
    var mapCanvas = document.getElementById("PipoMap");
    var mapOptions = { center: myCenter, zoom: 14 };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    var marker = new google.maps.Marker({ position: myCenter });
    google.maps.event.addListener(map, 'click', function (e) {
        var x = e.latLng.lat();
        var y = e.latLng.lng();
        var PipoCoordinate = document.getElementById("PipoCoordinate");
        PipoCoordinate.value = x + ',' + y;
        if (marker != null) {
            marker.setMap(null);
        }
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(x, y),
            map: map
        });
    });
    marker.setMap(map);
}
