function redirectToMapsUrl(address) {
    let mapsUrl = "https://maps.google.com/maps?q=" + address;
    window.open(mapsUrl, '_blank');
}