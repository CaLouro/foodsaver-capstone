import 'address.dart';

class Business {
  String name;
  Address address;
  String contactEmail;
  String contactPhone;
  bool isFavorite;

  Business(
    this.name,
    this.address,
    this.contactEmail,
    this.contactPhone,
    this.isFavorite,
  );
}
