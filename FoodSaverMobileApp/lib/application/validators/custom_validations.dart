import 'package:flutter/widgets.dart';
import 'package:get/get.dart';

abstract class CustomValidations {
  static const String _passwordRegex =
      r'^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,128}$';

  static const int passwordMinLength = 6;
  static const int passwordMaxLength = 128;

  static FormFieldValidator<T> username<T>({String? errorText}) {
    return (T? valueCandidate) {
      if (valueCandidate == null ||
          valueCandidate is! String ||
          !GetUtils.isUsername(valueCandidate)) {
        return errorText ?? 'Value is not a valid username.';
      }
      return null;
    };
  }

  static FormFieldValidator<String> password({String? errorText}) {
    return (String? valueCandidate) {
      if (valueCandidate == null ||
          !RegExp(_passwordRegex).hasMatch(valueCandidate)) {
        return errorText ?? 'Value is not a valid password';
      }
      return null;
    };
  }

  static FormFieldValidator<T> passwordMatch<T>(
    T? Function() getter, {
    String? errorText,
  }) {
    return (T? valueCandidate) {
      final other = getter();
      if (valueCandidate == null ||
          other == null ||
          valueCandidate.runtimeType != other.runtimeType ||
          valueCandidate != other) {
        return errorText ?? 'Value does not match.';
      }
      return null;
    };
  }
}
