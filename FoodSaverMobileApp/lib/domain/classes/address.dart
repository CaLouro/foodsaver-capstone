class Address {
  String addressLine;
  String city;
  String provinceCode;
  String postalCode;

  Address(this.addressLine, this.city, this.provinceCode, this.postalCode);

  String presentAddress({
    bool showCity = true,
    bool showProvince = false,
    bool showPostalCode = false,
  }) {
    StringBuffer buffer = StringBuffer(addressLine);

    if (showCity) {
      buffer.writeAll([', ', city]);
    }

    if (showProvince) {
      buffer.writeAll([', ', provinceCode]);
    }

    if (showPostalCode) {
      buffer.writeAll([', ', postalCode]);
    }

    return buffer.toString();
  }
}
