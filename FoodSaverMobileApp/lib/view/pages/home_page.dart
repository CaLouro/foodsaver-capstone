import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../application/services/auth_service_interface.dart';

class HomePage extends StatelessWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Saver'),
      ),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            Get.find<IAuthService>()
                .signOut()
                .then((value) => Get.offAllNamed('/login'));
          },
          child: const Text('Log out'),
        ),
      ),
    );
  }
}
