import 'package:flutter/material.dart';
import 'package:universitymobileapp/data/UserService.dart';

class SignupScreen extends StatefulWidget {
  const SignupScreen({super.key});

  @override
  State<SignupScreen> createState() => _SignupScreenState();
}

class _SignupScreenState extends State<SignupScreen> {
  final TextEditingController nameController = TextEditingController();
  final TextEditingController usernameController = TextEditingController();
  final TextEditingController emailController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();

  final UserService userService = UserService();
  bool isLoading = false;

  void register(BuildContext context) async {
    String name = nameController.text.trim();
    String username = usernameController.text.trim();
    String email = emailController.text.trim();
    String password = passwordController.text;

    if (name.isEmpty|| username.isEmpty || email.isEmpty || password.isEmpty){
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('please fill in all fields')),
      );
      return;
    }

    setState(() {
      isLoading = true;
    });

    try {
      final user = await userService.register(name, username, email, password);
      if(!context.mounted) return;

      if(user != null){
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Registration successful! Please log in.'),
            backgroundColor: Colors.green,
            duration: Duration(seconds: 3),
          ),
        );
        Navigator.pop(context);
      }
      else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('Username or email already exists'),
            behavior: SnackBarBehavior.floating,
            duration: const Duration(seconds: 3),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(20),
            ),
            backgroundColor: Colors.black38,
          ),
        );
      }
      }
    catch (e){
      if (!context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Failed to connect to server'),
          backgroundColor: Colors.red,
        ),
      );

    }
    finally {
      if (mounted){
        setState(() {
          isLoading = false;
        });
      }
    }

  }

@override
void dispose(){
    nameController.dispose();
    usernameController.dispose();
    emailController.dispose();
    passwordController.dispose();
    super.dispose();
}


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Sign up'),
        backgroundColor: Colors.indigo,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              const SizedBox(height: 30),
              const Icon(Icons.person_add, size: 100, color:Colors.indigo),
              const SizedBox(height: 20),

              TextField(
                controller: nameController,
                decoration: const InputDecoration(
                  labelText: 'Full Name',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.badge),
                ),
              ),

              const SizedBox(height: 20),

              TextField(
                controller: usernameController,
                decoration: const InputDecoration(
                  labelText: 'Username',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.person),
                ),
              ),

              const SizedBox(height: 20),

              TextField(
                controller: emailController,
                decoration: const InputDecoration(
                  labelText: 'Email',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.email),
                ),
              ),

              const SizedBox(height: 20),

              TextField(
                controller: passwordController,
                decoration: const InputDecoration(
                  labelText: 'Password',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.password),
                ),
              ),

              const SizedBox(height: 20),

              ElevatedButton(
                  onPressed: isLoading ? null : () => register(context),
                  child: isLoading
                      ? const CircularProgressIndicator()
                      : const Text('Sign up'),
              ),

              const SizedBox(height: 20),


              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text('Already have an account?'),
                  GestureDetector(
                    onTap: () {
                      Navigator.pop(context);
                    },
                    child: const Text(
                      ' Login',
                      style: TextStyle(
                        decoration: TextDecoration.underline,
                        color: Colors.blue,
                      ),
                    ),
                  )
                ],
              )

            ],
          )
        ),
      ),
    );
  }
}
