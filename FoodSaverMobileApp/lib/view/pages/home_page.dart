import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../application/controllers/home_controller.dart';
import '../widgets/deals_home_tab.dart';
import '../widgets/restaurant_home_tab.dart';
import '../widgets/settings_home_tab.dart';

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  final HomeController controller = Get.put(HomeController());

  final Map<int, Widget> bodyTabs = const {
    0: RestaurantHomeTab(),
    1: DealsHomeTab(),
    2: SettingsHomeTab(),
  };

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Saver'),
      ),
      body: Obx(() => bodyTabs[controller.currentTabIndex.value]!),
      bottomNavigationBar: Obx(
        () => BottomNavigationBar(
          currentIndex: controller.currentTabIndex.value,
          onTap: controller.changeCurrentTabIndex,
          items: const [
            BottomNavigationBarItem(
              icon: Icon(Icons.restaurant_rounded),
              label: 'Restaurants',
            ),
            BottomNavigationBarItem(
              icon: Icon(Icons.discount_rounded),
              label: 'Deals',
            ),
            BottomNavigationBarItem(
              icon: Icon(Icons.settings_rounded),
              label: 'Settings',
            ),
          ],
        ),
      ),
    );
  }
}
