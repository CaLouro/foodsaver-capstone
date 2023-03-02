import 'package:flutter/foundation.dart';
import 'package:get/get.dart';

import '../../domain/classes/address.dart';
import '../../domain/classes/business.dart';
import '../../domain/exceptions/auth_exception.dart';
import '../../domain/services/auth_service_interface.dart';
import '../../view/utils/error_snackbar.dart';
import '../core/app_messages.dart';

class HomeController extends GetxController {
  final RxInt currentTabIndex = RxInt(0);
  final RxBool isLoading = RxBool(true);
  final ObserverList<Business> businesses = ObserverList();

  @override
  void onInit() {
    //  Temporary static values for UI testing.
    isLoading.toggle();
    businesses.add(
      Business(
        'Vlad\'s Kimchi Shop',
        Address('429 Tamarack Dr', 'Waterloo', 'ON', 'N2L 4G9'),
        'vlad@kimchii.ca',
        '(123) 123-1234',
        false,
      ),
    );
    businesses.add(
      Business(
        'Caio\'s Nugget Shop',
        Address('429 Tamarack Dr', 'Waterloo', 'ON', 'N2L 4G9'),
        'vlad@kimchii.ca',
        '(123) 123-1234',
        true,
      ),
    );
    businesses.add(
      Business(
        'Keenan\'s Petshop',
        Address('429 Tamarack Dr', 'Waterloo', 'ON', 'N2L 4G9'),
        'vlad@kimchii.ca',
        '(123) 123-1234',
        false,
      ),
    );

    super.onInit();
  }

  void changeCurrentTabIndex(int value) {
    currentTabIndex.value = value;
  }

  Future<void> signOut() async {
    try {
      await Get.find<IAuthService>().signOut();
      Get.offAllNamed('/login');
    } on AuthException catch (e) {
      ErrorSnackbar(AppMessages.localizeAuthExceptionMessage(e)).show();
    } catch (_) {
      const ErrorSnackbar(AppMessages.genericErrorMessage).show();
    }
  }

  void favoriteBusiness(Business business) {
    print('favoriteBusiness: ${business.name} - ${business.isFavorite}');
  }

  void navigateToBusinessDetails(Business business) {
    print('navigateToBusinessDetails: ${business.name}');
  }
}
