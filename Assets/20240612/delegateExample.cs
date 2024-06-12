using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 델리게이트 제작
public delegate void CalculationCompletedEventHandler(int result);
public delegate void CalculationCompletedEventHandler2(int result);

public class TestClass
{
    public CalculationCompletedEventHandler2 CalculationCompleted2;
    
    public void TestClassFunc(int result)
    {
        Debug.Log($"result : {result}");
    }

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
    // 델리게이트 선언
    public CalculationCompletedEventHandler CalculationCompleted;

    // Add
    public void Add(int a, int b)
    {
        int result = a + b;
        CalculationCompleted?.Invoke(result);
    }

    // Subtract
    public void Subtract(int a, int b)
    {
        int result = a - b;
        CalculationCompleted?.Invoke(result);
    }
}
public class delegateExample : MonoBehaviour
{
    private Calculator _calculator;
    private int sumResult = 0;
    
    public TestClass testClass;

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator CalculatorDelayPrint()
    {
        yield return new WaitForSeconds(10.0f);
        
        testClass?.CalculationCompleted2?.Invoke(10);
    }
    
    void Start()
    {
        _calculator = new Calculator();
        testClass = new TestClass();
        
        // delegateExample의 클래스에 CalculationCompletedEventHandler 함수를 등록
        _calculator.CalculationCompleted += CalculationCompletedEventHandler;
        _calculator.CalculationCompleted += testClass.TestClassFunc;

        // 코루틴 호출
        StartCoroutine(CalculatorDelayPrint());
    }

    void CalculationCompletedEventHandler(int result)
    {
        Debug.Log($"Calculation completed. Result: {result}");
        
        // sumResult의 변수값을 결과값으로 변경
        sumResult = result;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _calculator?.Add(sumResult, 10);
        }

        if (testClass != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                testClass.CalculationCompleted2 += testClass.AddResult;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                testClass.CalculationCompleted2 += testClass.SubtractResult;
            }
        }
        
        // 델리게이트에서 빼기
        if (sumResult >= 100 && _calculator is { CalculationCompleted: not null })
        {
            _calculator.CalculationCompleted -= CalculationCompletedEventHandler;
            Debug.Log("Finish");
        }
    }
}
