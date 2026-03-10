// ---------------------------------------------------------------------------
// Copyright (C) 2005 Microsoft Corporation - All Rights Reserved
// ---------------------------------------------------------------------------

using System.CodeDom;
using System.Runtime.Serialization;
using System.Security.Permissions;
using LogicBuilder.Workflow.ComponentModel.Compiler;
using System;

namespace LogicBuilder.Workflow.Activities.Rules
{
    #region RuleException
    /// <summary>
    /// Represents the base class for all rule engine exception classes
    /// </summary>
    [Serializable]
    public class RuleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RuleException class
        /// </summary>
        public RuleException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleException class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public RuleException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleException class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="ex">The inner exception</param>
        public RuleException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        /// Constructor required by for Serialization - initialize a new instance from serialized data
        /// </summary>
        /// <param name="serializeInfo">Reference to the object that holds the data needed to deserialize the exception</param>
        /// <param name="context">Provides the means for deserializing the exception data</param>
        protected RuleException(SerializationInfo serializeInfo, StreamingContext context)
            : base(serializeInfo, context)
        {
        }
    }
    #endregion

    #region RuleEvaluationException
    /// <summary>
    /// Represents the the exception thrown when an error is encountered during evaluation
    /// </summary>
    [Serializable]
    public class RuleEvaluationException : RuleException
    {
        /// <summary>
        /// Initializes a new instance of the RuleRuntimeException class
        /// </summary>
        public RuleEvaluationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleRuntimeException class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public RuleEvaluationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleRuntimeException class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="ex">The inner exception</param>
        public RuleEvaluationException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        /// Constructor required by for Serialization - initialize a new instance from serialized data
        /// </summary>
        /// <param name="serializeInfo">Reference to the object that holds the data needed to deserialize the exception</param>
        /// <param name="context">Provides the means for deserializing the exception data</param>
        protected RuleEvaluationException(SerializationInfo serializeInfo, StreamingContext context)
            : base(serializeInfo, context)
        {
        }
    }
    #endregion

    #region RuleEvaluationIncompatibleTypesException
    /// <summary>
    /// Represents the exception thrown when types are incompatible
    /// </summary>
    [Serializable]
    public class RuleEvaluationIncompatibleTypesException : RuleException
    {
        /// <summary>
        /// Type on the left of the operator
        /// </summary>
        public Type Left { get; set; }

        /// <summary>
        /// Arithmetic operation that failed
        /// </summary>
        public CodeBinaryOperatorType Operator { get; set; }

        /// <summary>
        /// Type on the right of the operator
        /// </summary>
        public Type Right { get; set; }

        /// <summary>
        /// Initializes a new instance of the RuleEvaluationIncompatibleTypesException class
        /// </summary>
        public RuleEvaluationIncompatibleTypesException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleEvaluationIncompatibleTypesException class
        /// </summary>
        /// <param name="message"></param>
        public RuleEvaluationIncompatibleTypesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleEvaluationIncompatibleTypesException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public RuleEvaluationIncompatibleTypesException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleEvaluationIncompatibleTypesException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="left"></param>
        /// <param name="op"></param>
        /// <param name="right"></param>
        public RuleEvaluationIncompatibleTypesException(
            string message,
            Type left,
            CodeBinaryOperatorType op,
            Type right)
            : base(message)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        /// <summary>
        /// Initializes a new instance of the RuleEvaluationIncompatibleTypesException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="left"></param>
        /// <param name="op"></param>
        /// <param name="right"></param>
        /// <param name="ex"></param>
        public RuleEvaluationIncompatibleTypesException(
            string message,
            Type left,
            CodeBinaryOperatorType op,
            Type right,
            Exception ex)
            : base(message, ex)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        /// <summary>
        /// Constructor required by for Serialization - initialize a new instance from serialized data
        /// </summary>
        /// <param name="serializeInfo">Reference to the object that holds the data needed to deserialize the exception</param>
        /// <param name="context">Provides the means for deserializing the exception data</param>
        protected RuleEvaluationIncompatibleTypesException(SerializationInfo serializeInfo, StreamingContext context)
            : base(serializeInfo, context)
        {
            if (serializeInfo == null)
                throw new ArgumentNullException("serializeInfo");
            string qualifiedTypeString = serializeInfo.GetString("left");
            if (qualifiedTypeString != "null")
                Left = Type.GetType(qualifiedTypeString);
            Operator = (CodeBinaryOperatorType)serializeInfo.GetValue("op", typeof(CodeBinaryOperatorType));
            qualifiedTypeString = serializeInfo.GetString("right");
            if (qualifiedTypeString != "null")
                Right = Type.GetType(qualifiedTypeString);
        }
    }
    #endregion

    #region RuleSetValidationException
    /// <summary>
    /// Represents the exception thrown when a ruleset can not be validated
    /// </summary>
    [Serializable]
    public class RuleSetValidationException : RuleException
    {
        private readonly ValidationErrorCollection m_errors;

        /// <summary>
        /// Collection of validation errors that occurred while validating the RuleSet
        /// </summary>
        public ValidationErrorCollection Errors
        {
            get { return m_errors; }
        }

        /// <summary>
        /// Initializes a new instance of the RuleSetValidationException class
        /// </summary>
        public RuleSetValidationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleSetValidationException class
        /// </summary>
        /// <param name="message"></param>
        public RuleSetValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleSetValidationException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public RuleSetValidationException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuleSetValidationException class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public RuleSetValidationException(
            string message,
            ValidationErrorCollection errors)
            : base(message)
        {
            m_errors = errors;
        }

        /// <summary>
        /// Constructor required by for Serialization - initialize a new instance from serialized data
        /// </summary>
        /// <param name="serializeInfo">Reference to the object that holds the data needed to deserialize the exception</param>
        /// <param name="context">Provides the means for deserializing the exception data</param>
        protected RuleSetValidationException(SerializationInfo serializeInfo, StreamingContext context)
            : base(serializeInfo, context)
        {
            if (serializeInfo == null)
                throw new ArgumentNullException("serializeInfo");
            m_errors = (ValidationErrorCollection)serializeInfo.GetValue("errors", typeof(ValidationErrorCollection));
        }
    }
    #endregion
}
