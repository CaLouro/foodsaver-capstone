import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../application/controllers/business_info_controller.dart';

class BusinessInfoPage extends StatefulWidget {
  const BusinessInfoPage({Key? key}) : super(key: key);

  @override
  State<BusinessInfoPage> createState() => _BusinessInfoPageState();
}

class _BusinessInfoPageState extends State<BusinessInfoPage> {
  final controller = Get.put(BusinessInfoController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Business'),
      ),
      body: const Placeholder(),
    );
  }
}
