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
}
