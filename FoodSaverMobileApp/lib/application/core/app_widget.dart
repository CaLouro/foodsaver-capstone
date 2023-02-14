import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'app_bindings.dart';
import 'app_navigation.dart';

class AppWidget extends StatelessWidget {
  const AppWidget({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
      title: 'Food Saver',
      locale: const Locale('en', 'CA'),
      initialBinding: AppBindings(),
      initialRoute: AppNavigation.initialPath,
      getPages: AppNavigation.pages,
    );
  }
}
