#include <iostream>
#include <vector>

using namespace std;

#define byte uint8_t //C# reference :33333

#define byte_2 uint16_t
#define byte_3 uint32_t

namespace Field_theory{

    /// <summary>
    ///     GF ( 2^8 ), where 2 - field characteristics, 8 - field order
    /// </summary>
static class Field_Galois 
{   
private:
    static const size_t field_charac = 0b10;
    static const size_t field_order = 0b1000;

    const size_t max_even = pow(field_charac,field_order) - field_charac;
public:
#pragma region Constructors + singlton
    Field_Galois(Field_Galois& other) = delete;

    void operator=(const Field_Galois&) = delete;
    void operator+=(const Field_Galois&) = delete;
    void operator-=(const Field_Galois&) = delete;
    void operator*=(const Field_Galois&) = delete;

    Field_Galois()//TODO: синглтон + легковес(мб построить дерево типов)
    {
        irred_poly();
    }

    ~Field_Galois() {
        ir_poly.clear();
    }
#pragma endregion
    
    /// <summary>
    /// Getting product of polis
    /// </summary>
    /// <param name="a"> - first poly</param>
    /// <param name="b"> - second poly</param>
    /// <param name="modulo"> - module over the ring</param>
    /// <returns>polynom</returns>
    static byte_2 multiply(byte_2 a, byte_2 b, byte_2 modulo)  {
        
        byte_2 result = 0;  byte_3 iter=1;

        for (size_t i = 0; i < field_order; i++) result ^= a * (b & (iter <<= 1));
        
        return remnant(result, modulo);
    }
    /// <summary>
    /// Getting sum of polis
    /// </summary>
    /// <param name="a"> first poly</param>
    /// <param name="b"> second poly</param>
    /// <returns>result polynom</returns>
    static byte add(byte a, byte b){
        return (a ^ b);
    }
    /// <summary>
    /// Getting
    /// </summary>
    /// <param name="poly">Polynom</param>
    /// <returns>Col of non zero a * x^p / degree of polynom :333</returns>
    static size_t degree(byte_2 poly){
        size_t res=0;

        for (; poly > 0; poly >>= 0b1)  if ((poly & 0b1) == 0b1) res++;
        //0b101010
        return res;
    }
    /// <summary>
    /// getting remnant of poly by module
    /// </summary>
    /// <param name="poly">Polynom</param>
    /// <param name="module">Module over the ring</param>
    /// <returns>Remnant of poly by module</returns>
    static byte remnant(byte_2 poly, byte_2 module) {
        size_t module_count = degree(module);

        while (size_t dif = degree(poly) - module_count >= 0)   poly ^= module << dif;
        
        return poly;
    }
    /// <summary>
    /// Method for getting inverse of the required polynom
    /// </summary>
    /// <param name="poly">the required polynom</param>
    /// <param name="modulo">Module over the ring</param>
    /// <returns> polynom </returns>
    byte_2 get_inverse(byte poly, byte_2 modulo) const
    {

        byte copy_poly(poly);
        int degree_pow = degree(max_even);
        byte bit(max_even);

        for (int i = degree_pow; i >= 0; i--, bit >>= 1)
        {
            if (modulo & bit)   poly = multiply(poly, copy_poly, modulo);
            
            poly = multiply(poly, poly, modulo);
            
        }
        return poly;
    }

    /// <summary>
    /// Get a list of irreducible polynoms
    /// </summary>
    /// <returns>Vector of polynomes</returns>
    static vector<byte_2> get_polis(){
        return ir_poly;
    }
    
    /// <summary>
    /// Calling in constructor
    ///
    /// pushing in vector first 30 irreducible polynoms
    /// </summary>
    static void irred_poly()
    {
        size_t i = 0;
        for (byte_2 number = 0b100000000; number <= 0b111111111; number +=2) 
        {
            if (i == 30) return;
            int null_remannts = 0;

            for (byte j = 0b10; j < 0b100000; j++)
                if (!remnant(number, j))  break;
                else null_remannts++;

            if (null_remannts){
                ir_poly.push_back(number);
                i++;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poly">polynom thats should be checked on irreducibility</param>
    /// <returns> true if poltnom is irreducible, false else </returns>
    static bool if_irreducible(byte_2 poly)
    {
        size_t counter = 0;
        byte_2 bit = 0b1;
        size_t _degree = degree(poly);

        for (size_t i = 0; i <= _degree; i++, bit <<= 1)
            if (bit & poly)     counter++;
        // 0 
        return (counter & 0b1) && !counter);
    }
private:
    /// <summary>
    /// vector of irreducible polynoms
    /// </summary>
    static std::vector<byte_2> ir_poly;

    /// <summary>
    /// instance of field
    /// </summary>
    static Field_Galois* instance;
};
Field_Galois* Field_Galois::instance = nullptr;

}
int main()
{   
    Field_theory::Field_Galois a;
    
    cout<<a.add(255, 255) << endl;

    cout << a.multiply(255, 5, 283)<<endl;

    cout << a.multiply(255, 5, 283) << endl;

    cout << a.get_inverse(255,283) << endl;

    for (auto& var : a.get_polis())
        cout << var << endl;

    return 0;
}
