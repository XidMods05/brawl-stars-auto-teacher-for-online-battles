import re

input_file_name: str = "libg.so.c"
output_file_name: str = "edited_libg.so.c"

replacements: dict = {}
maxValueToBitsCountDict: dict = {1: 1, 3: 2, 7: 3, 15: 4, 31: 5, 63: 6, 127: 7, 255: 8, 511: 9, 1023: 10, 2047: 11,
                                 4095: 12,
                                 8191: 13, 16383: 14, 32767: 15, 65535: 16, 131071: 17, 262143: 18, 524287: 19,
                                 1048575: 20,
                                 2097151: 21, 4194303: 22, 8388607: 23, 16777215: 24, 33554431: 25, 67108863: 26,
                                 134217727: 27}

with open(input_file_name, 'r') as file:
    data = file.readlines()


def rename_by_key_value_pair(lnx: str, idx: int):
    try:
        if len(data) > idx + 2:
            search_string = "(a1, a2,"
            for k, v in maxValueToBitsCountDict.items():
                if f"{search_string} {v});" in data[idx + 2]:
                    oldLine = lnx[lnx.index("sub_"):lnx.index("(_")]
                    pseudoSymbols = re.sub(r'sub_.*?\(', 'sub_(', lnx)

                    if "return" in data[idx + 2]:
                        pseudoSymbols = pseudoSymbols.replace("sub_", f"BitStream::writeIntMax{k}")
                    else:
                        pseudoSymbols = pseudoSymbols.replace("sub_", f"BitStream::writePositiveIntMax{k}")

                    replacements.update(
                        {oldLine: pseudoSymbols[pseudoSymbols.index("BitStream::"):pseudoSymbols.index("(_")]})
    except:
        pass

    return lnx


print("operation number 1...")
output = []
for index, line in enumerate(data):
    output.append(rename_by_key_value_pair(line, index))
    if len(output) % 100000 == 0:
        print(len(output))

print("operation number 2...")
for key, value in replacements.items():
    output = "".join(output).replace(key, value)

print("operation number 3...")
with open(output_file_name, 'w') as file:
    file.write("".join(output))
