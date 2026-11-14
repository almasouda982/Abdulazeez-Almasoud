import 'package:flutter/material.dart';
import '../../App2/data/waitlistEntryService.dart';

class WaitlistEntryProvider extends ChangeNotifier {
  final WaitlistEntryService waitlistService = WaitlistEntryService();

  List<dynamic> waitlistEntries = [];
  bool isLoading = false;
  String? error;

  Future<void> fetchWaitlistEntries() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      waitlistEntries = await waitlistService.getWaitlistEntrys();
    } catch (e) {
      error = 'Failed to load waitlist entries: $e';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}