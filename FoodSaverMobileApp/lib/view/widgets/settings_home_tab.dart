import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../application/controllers/home_controller.dart';

class SettingsHomeTab extends StatelessWidget {
  const SettingsHomeTab({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final HomeController controller = Get.find();

    return ListView(
      padding: const EdgeInsets.all(16),
      children: [
        ListTile(
          title: const Text('Sign Out'),
          leading: const Icon(Icons.logout_rounded),
          onTap: controller.signOut,
          textColor: Colors.red,
          iconColor: Colors.red,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(6),
          ),
          tileColor: Colors.red[50],
        ),
      ],
    );
  }
}
