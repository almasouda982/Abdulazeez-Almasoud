import 'dart:convert';
import 'package:http/http.dart' as http;

import 'connection.dart';


class UserService {


// Login Integration
  Future<Map<String, dynamic>?> login(String username, String password) async {

    final response = await http.post(
      Uri.parse(ConnectionString.login),

      headers: {
        'Content-Type': 'application/json',
      },

      body: json.encode({
        'username': username,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);;
    } else if (response.statusCode == 401) {
      return null;
    } else {
      throw Exception('Failed to login');
    }
  }




// Register Integration
  Future<Map<String, dynamic>?> register(
      String name,
      String username,
      String email,
      String password
      ) async {

    final response = await http.post(
      Uri.parse(ConnectionString.register),

      headers: {
        'Content-Type': 'application/json',
      },

      body: json.encode({
        'name': name,
        'username': username,
        'email': email,
        'password': password,
      }),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else if (response.statusCode == 409) {
      return null;
    } else {
      throw Exception('Registration failed: ${response.body}');
    }
  }
}