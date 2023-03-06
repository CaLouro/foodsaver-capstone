import 'package:get/get.dart';

import '../../domain/classes/business.dart';

class BusinessInfoController extends GetxController {
  late final Business? _business;

  @override
  void onInit() {
    super.onInit();

    // Get Business from route arguments
    _business = Get.arguments is Business ? Get.arguments : null;
  }

  String get businessName => _business?.name ?? '';

  bool get businessIsFavorite => _business?.isFavorite ?? false;

  String get businessAddress =>
      _business?.address.presentAddress(showPostalCode: true) ?? '';

  String get businessEmailContact => _business?.contactEmail ?? '';

  String get businessPhoneContact => _business?.contactPhone ?? '';

  void onFavoriteIconPress(bool favoriteState) {
    _business?.isFavorite = favoriteState;
  }
}
