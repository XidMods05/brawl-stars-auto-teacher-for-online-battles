namespace SimpleTransClassesOfPostNum1.SimpleTools;

// original version URL: https://github.com/Mimi8298/Supercell.Magic/blob/master/Supercell.Magic.Titan/RC4Encrypter.cs

public class Rc4Encrypter : StreamEncrypter
{
    private byte[] _key;
    private byte _x;
    private byte _y;

    public Rc4Encrypter(string baseKey, string nonce)
    {
        _key = [];
        InitState(baseKey, nonce);
    }

    public void InitState(string baseKey, string nonce)
    {
        var key = baseKey + nonce;

        _key = new byte[256];
        _x = 0;
        _y = 0;

        for (var i = 0; i < 256; i++) _key[i] = (byte)i;

        for (int i = 0, j = 0; i < 256; i++)
        {
            j = (byte)(j + _key[i] + key[i % key.Length]);
            (_key[i], _key[j]) = (_key[j], _key[i]);
        }

        for (var i = 0; i < key.Length; i++)
        {
            _x += 1;
            _y += _key[_x];
            (_key[_y], _key[_x]) = (_key[_x], _key[_y]);
        }
    }

    public int Decrypt(byte[] input, byte[] output, int length)
    {
        return Encrypt(input, output, length);
    }

    public int Encrypt(byte[] input, byte[] output, int length)
    {
        for (var i = 0; i < length; i++)
        {
            _x += 1;
            _y += _key[_x];

            (_key[_y], _key[_x]) = (_key[_x], _key[_y]);
            output[i] = (byte)(input[i] ^ _key[(byte)(_key[_x] + _key[_y])]);
        }

        return 0;
    }

    public override int GetEncryptionOverhead()
    {
        throw new NotImplementedException();
    }

    public void Destruct()
    {
        _key = null!;
        _x = 0;
        _y = 0;
    }
}