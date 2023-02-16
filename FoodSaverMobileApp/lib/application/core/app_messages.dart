import '../../domain/exceptions/auth_exception.dart';

abstract class AppMessages {
  static const String genericErrorMessage =
      'Something went wrong. Please try again.';

  static String localizeAuthExceptionMessage(AuthException exception) {
    switch (exception.code) {
      case 'invalid-email':
        return 'Invalid email format. Please try again.';

      case 'user-disabled':
        return 'User is disabled.';

      case 'user-not-found':
        return 'There is no account with this credentials.';

      case 'wrong-password':
        return 'Wrong password. Please try again.';

      case 'email-already-in-use':
        return 'Email already in use.';

      case 'operation-not-allowed':
        return 'Operation not allowed.';

      case 'weak-password':
        return 'Weak password. The password must follow the specification.';

      default:
        return genericErrorMessage;
    }
  }
}
