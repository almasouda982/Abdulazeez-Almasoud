import 'package:flutter/material.dart';
import '../../data/bookService.dart';
import '../../data/connection.dart';
import '../../data/courseBookService.dart';
import 'detailScreens/courseBookDetailsScreen.dart';

class CourseBookScreen extends StatefulWidget {
  final int courseId;
  final String courseTitle;

  const CourseBookScreen({required this.courseId, required this.courseTitle, super.key});

  @override
  State<CourseBookScreen> createState() => _CourseBookScreenState();
}

class _CourseBookScreenState extends State<CourseBookScreen> {
  final CourseBookService courseBookService = CourseBookService();
  final BookService bookService = BookService();
  late Future<List<dynamic>> courseBooks;

  @override
  void initState() {
    super.initState();
    loadBooks();
  }

  void loadBooks() {
    courseBooks = courseBookService.getBooksForCourse(widget.courseId);
  }

  Future<void> deleteBook(int bookId) async {
    try {
      await courseBookService.removeBookFromCourse(widget.courseId, bookId);

      if (!mounted) return;

      setState(() {
        loadBooks();
      });

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Book successfully removed from course')),
      );
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error: $e')),
      );
    }
  }

  void _showAddBookDialog(List<dynamic> currentAssignedBooks) async {
    try {
      final allBooks = await bookService.getBooks();

      // Extract IDs of books already assigned to this course
      final assignedBookIds = currentAssignedBooks.map((cb) {
        final bData = cb['book'] ?? cb['Book'] ?? {};
        return cb['bookId'] ?? cb['BookId'] ?? bData['id'] ?? bData['Id'];
      }).toSet();

      final availableBooks = allBooks.where((book) {
        final bookId = book['id'] ?? book['Id'];
        return !assignedBookIds.contains(bookId);
      }).toList();

      if (!mounted) return;

      showDialog(
        context: context,
        builder: (context) {
          return AlertDialog(
            title: const Text('Assign Book to Course'),
            content: SizedBox(
              width: double.maxFinite,
              height: 350,
              child: availableBooks.isEmpty
                  ? const Center(
                child: Text(
                  'All available books are already assigned to this course.',
                  textAlign: TextAlign.center,
                  style: TextStyle(color: Colors.grey),
                ),
              )
                  : ListView.builder(
                itemCount: availableBooks.length,
                itemBuilder: (context, index) {
                  final book = availableBooks[index];
                  final bookId = book['id'] ?? book['Id'];
                  final bookTitle = book['title'] ?? book['Title'] ?? 'Unknown Book';
                  final bookAuthor = book['author'] ?? book['Author'] ?? 'N/A';

                  return ListTile(
                    title: Text(bookTitle),
                    subtitle: Text('Author: $bookAuthor'),
                    trailing: ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.indigo,
                        foregroundColor: Colors.white,
                      ),
                      onPressed: () async {
                        try {
                          await courseBookService.addBookToCourse(widget.courseId, bookId);

                          if (!mounted) return;
                          Navigator.pop(context); // Close dialog

                          setState(() {
                            loadBooks(); // Refresh list
                          });

                          ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(content: Text('Book successfully added to course')),
                          );
                        } catch (e) {
                          Navigator.pop(context);
                          ScaffoldMessenger.of(context).showSnackBar(
                            SnackBar(content: Text('Error adding book: $e')),
                          );
                        }
                      },
                      child: const Text('Add'),
                    ),
                  );
                },
              ),
            ),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: const Text('Cancel'),
              ),
            ],
          );
        },
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Failed to load available books: $e')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('${widget.courseTitle} - Books'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: courseBooks,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(child: CircularProgressIndicator());
          }

          if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          }

          final booksList = snapshot.data ?? [];

          return Scaffold(
            floatingActionButton: FloatingActionButton(
              onPressed: () => _showAddBookDialog(booksList),
              backgroundColor: Colors.indigo,
              child: const Icon(Icons.add, color: Colors.white),
            ),
            body: booksList.isEmpty
                ? const Center(
              child: Text(
                'No books assigned to this course yet.\nTap + to assign books.',
                textAlign: TextAlign.center,
                style: TextStyle(color: Colors.grey, fontSize: 16),
              ),
            )
                : Column(
              children: [
                Expanded(
                  child: ListView.builder(
                    padding: const EdgeInsets.all(12),
                    itemCount: booksList.length,
                    itemBuilder: (context, index) {
                      final courseBook = booksList[index];

                      final bookData = courseBook['book'] ?? courseBook['Book'] ?? {};

                      final title = bookData['title'] ?? bookData['Title'] ?? 'N/A';
                      final author = bookData['author'] ?? bookData['Author'] ?? 'N/A';
                      final price = bookData['price'] ?? bookData['Price'] ?? 0.00;

                      final bookId = courseBook['bookId'] ?? courseBook['BookId'] ?? bookData['id'] ?? bookData['Id'];

                      final String imagePath = bookData['imageUrl']?.toString() ?? bookData['ImageUrl']?.toString() ?? '';
                      final String imageUrl = imagePath.isEmpty
                          ? ''
                          : Uri.parse(ConnectionString.dashboard)
                          .resolve(imagePath)
                          .toString();

                      return Card(
                        elevation: 3,
                        margin: const EdgeInsets.symmetric(vertical: 8),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: ListTile(
                          contentPadding: const EdgeInsets.all(10),
                          leading: ClipRRect(
                            borderRadius: BorderRadius.circular(6),
                            child: imageUrl.isNotEmpty
                                ? Image.network(
                              imageUrl,
                              width: 50,
                              height: 70,
                              fit: BoxFit.cover,
                              errorBuilder: (context, error, stackTrace) {
                                return Container(
                                  width: 50,
                                  height: 70,
                                  color: Colors.grey[200],
                                  child: const Icon(Icons.book, size: 24),
                                );
                              },
                            )
                                : Container(
                              width: 50,
                              height: 70,
                              color: Colors.grey[200],
                              child: const Icon(Icons.book, size: 24),
                            ),
                          ),
                          title: Text(
                            title.toString(),
                            style: const TextStyle(fontWeight: FontWeight.bold),
                          ),
                          subtitle: Text(
                            'Author: $author\nPrice: \$$price',
                          ),
                          isThreeLine: true,
                          trailing: IconButton(
                            icon: const Icon(Icons.delete, color: Colors.red),
                            onPressed: () {
                              if (bookId != null) {
                                deleteBook(bookId);
                              }
                            },
                          ),
                          onTap: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => CourseBookDetailsScreen(bookData),
                              ),
                            );
                          },
                        ),
                      );
                    },
                  ),
                ),
                Container(
                  padding: const EdgeInsets.all(20),
                  width: double.infinity,
                  color: Colors.grey.shade100,
                  child: Text(
                    'Total Books Cost: \$${booksList.fold<double>(0.0, (sum, cb) {
                      final bData = cb['book'] ?? cb['Book'] ?? {};
                      return sum + ((bData['price'] ?? bData['Price'] ?? 0) as num).toDouble();
                    }).toStringAsFixed(2)}',
                    textAlign: TextAlign.center,
                    style: const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      color: Colors.green,
                    ),
                  ),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}