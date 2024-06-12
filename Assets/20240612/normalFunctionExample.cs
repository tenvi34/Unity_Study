using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace normalFunctionExampleNS
{
    public class TestClass
    {
        public void AddResult(int result)
        {
            Debug.Log($"AddResult Func : {result}");
        }

        public void SubtractResult(int result)
        {
            Debug.Log($"SubtractResult Func : {result}");
        }
    }

    public class Calculator
    {
        public string functionName = string.Empty;
        
        public TestClass testClass;
        // Add
        public void Add(int a, int b)
        {
            int result = a + b;
            testClass.AddResult(result);
        }

        // Subtract
        public void Subtract(int a, int b)
        {
            int result = a - b;
            testClass.SubtractResult(result);
        }
        
        public IEnumerator CalculatorDelayPrint()
        {
            yield return new WaitForSeconds(10.0f);

            if (functionName == "Add")
            {
                Add(3, 4);
            }
            else if (functionName == "Subtract")
            {
                Subtract(12, 5);
            }
        }
    }

    public class normalFunctionExample : MonoBehaviour
    {
        private Calculator _calculator;
        private int sumResult = 0;

        public TestClass testClass;
        
        void Start()
        {
            _calculator = new Calculator();
            testClass = new TestClass();

            _calculator.testClass = testClass;

            StartCoroutine(_calculator.CalculatorDelayPrint());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _calculator.functionName = "Add";
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _calculator.functionName = "Subtract";
            }
        }
    }
}
