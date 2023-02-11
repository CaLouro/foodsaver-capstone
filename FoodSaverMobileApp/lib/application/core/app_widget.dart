import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../view/pages/login_page.dart';

class AppWidget extends StatelessWidget {
  const AppWidget({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return const GetMaterialApp(
      title: 'Food Saver',
      home: LoginPage(),
    );
  }
}
