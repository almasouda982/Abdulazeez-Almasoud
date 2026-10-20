import 'package:flutter/material.dart';

class MyTextFormField extends StatelessWidget {
  MyTextFormField({super.key, required this.name});

  String name;

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      decoration: InputDecoration(
        labelText: name,
        enabledBorder: const OutlineInputBorder(
          borderSide: BorderSide(
            color: Colors.blue,
            width: 2,
          ),
          borderRadius: BorderRadius.all(Radius.circular(20)),
        ),
      ),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return "Please Enter your $name";
        }
        return null;
      },
    );
  }
}