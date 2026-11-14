import 'dart:convert';

import 'package:http/http.dart' as http;

import 'connection.dart';

class WaitlistEntryService {

  Future<List<dynamic>> getWaitlistEntrys() async {

    final response = await http.get(
      Uri.parse(ConnectionString.waitListEntry),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load waitlist entries');
    }
  }


}