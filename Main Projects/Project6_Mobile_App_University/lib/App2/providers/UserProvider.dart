import 'package:flutter/material.dart';
import '../../App1/data/UserService.dart';

class UserProvider extends ChangeNotifier {

  final UserService userService = UserService();

  Map<String, dynamic>? user;

  bool isLoading = false;

  String? error;






  Future<bool> register(
      String name,
      String email,
      String username,
      String password,
      ) async {

    isLoading = true;
    notifyListeners();

    try {

      final result =
      await userService.register(
        name,
        email,
        username,
        password,
      );

      return result != null;

    } finally {

      isLoading = false;
      notifyListeners();
    }
  }



  // Login
  Future<bool> login(
      String username,
      String password,
      ) async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      user = await userService.login(username, password);

      if (user == null) {
        error = 'Invalid username or password';
        return false;
      }

      return true;

    } catch (e) {
      error = 'Failed to connect to server';
      return false;

    } finally {
      isLoading = false;
      notifyListeners();
    }
  }

  // User Data
  String get username => user?['username']?.toString() ?? '';

  String get email => user?['email']?.toString() ?? '';

  int get userId => (user?['userId'] as num?)?.toInt() ?? 0;

  bool get isLoggedIn => user != null;

  // Logout
  void logout() {
    user = null;
    error = null;
    notifyListeners();
  }
}
