#include <iostream>
#include <vector>

#define byte uint8_t //C# reference :33333

#define byte8 uint16_t

namespace Field_theory{

    /// <summary>
    ///     <param>
    ///         p - характеристика поля, K - порядок поля
    ///         В арифметике полей Галуа неприводимым полиномом является аналог простых чисел.
    ///     </param>
    /// </summary>
class Field_Galua 
{
private:
    
public:
    
    Field_Galua(Field_Galua& other) = delete;

    void operator=(const Field_Galua&) = delete;

    Field_Galua()// синглтон + легковес(мб построить дерево типов)
    {
        if (instance != nullptr)
            delete this;
        else 
            instance = this;
    }

    ~Field_Galua() {
        Field.clear();
    }

    
    byte8 multiply(byte8 a, byte8 b, byte modulo) {
        return (a * b) & 0b00010001;
    }

    byte add(byte a, byte b) {
        return (a ^ b) << 1;
    }


    byte8 galoisMul(byte8 a, byte8 b) {
        byte8 mul = multiply(a, b, 1);
        const byte8 basePolynome = 0x100011011;
        mul ^= multiply(basePolynome, mul >> 48 << 16,0); // Вычитаем произведение Q(x) и старших блоков имеющегося произведения (сдвинутых на 4 блока), оставляя ненулевыми не более 12 младших блоков
        mul ^= multiply(basePolynome, mul >> 32,0); // Таким же образом получаем многочлен не более 8 степени, сохраняя остаток от деления. После этой операции именно остаток будет записан в mul.
        return mul;
    }
    byte8 Pow(byte8 a, unsigned n) {
        // Быстрое возведение в степень.
        if (n == 0) {
            return 1;
        }
        else if (n % 2 == 0) {
            return Pow(multiply(a, a,0), n / 2); // (a*a)^(n/2)
        }
        else {
            byte8 square = multiply(a, a,0);
            return multiply(Pow(square, n / 2), a,0); // a * (a*a)^[n/2] 
        }
    }
    byte8 Inverse(byte8 a, byte modulo) {
        return Pow(a, modulo);
    }
    byte8 extendTo(byte a) {
        return (a & 0xC3) * 0x249249 & 0x11000011 |
            (a & 0x1C) * 0x1240 & 0x00011100 |
            (a & 0x20) << 15;
    }

    std::string write() const{
        return "I'm boy";
    }

private:
    std::vector<byte> Field;
    std::vector<byte8> Fieldbyte8;
    std::vector<int> Fieldint;

    static Field_Galua* instance;
};
Field_Galua* Field_Galua::instance = nullptr;

}
int main()
{   
    Field_theory::Field_Galua a;
    a.write();

    Field_theory::Field_Galua c;
    c.write();

    Field_theory::Field_Galua b;
    b.write();

    return 0;
}
