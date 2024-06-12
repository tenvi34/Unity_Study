using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 델리게이트 제작
public delegate void CalculationCompletedEventHandler(int result);

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
    
    void Start()
    {
        _calculator = new Calculator();
        // delegateExample의 클래스에 CalculationCompletedEventHandler 함수를 등록
        _calculator.CalculationCompleted += CalculationCompletedEventHandler;
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
        
        // 델리게이트에서 빼기
        if (sumResult >= 100 && _calculator is { CalculationCompleted: not null })
        {
            _calculator.CalculationCompleted -= CalculationCompletedEventHandler;
            Debug.Log("Finish");
        }
    }
}
