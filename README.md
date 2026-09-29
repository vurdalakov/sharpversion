# SharpVersion

[SharpVersion](https://github.com/vurdalakov/sharpversion) is a command-line utility that shows .NET assembly information.

## Usage

* `sver <filename>`    - shows basic version information.
* `sver -a <filename>` - shows .NET assembly information - like assembly version or public key token.
* `sver -f <filename>` - shows standard file information - like size and last accessed time.
* `sver -v <filename>` - shows all fields of file version record - like file version and product name.
* `sver -p <filename>` - shows all of the above.

## Downloads

* From the [releases page](https://github.com/vurdalakov/sharpversion/releases).

## License

`SharpVersion` is distributed under the terms of the [MIT license](https://opensource.org/licenses/MIT).

## Examples

### Example output 1

```
sver -a System.Runtime.CompilerServices.Unsafe.dll
```

```
--- Assembly name:
Name:                     'System.Runtime.CompilerServices.Unsafe'
Version:                  '4.0.6.0'
Culture:                  'neutral'
Hash algorithm:           'SHA1'
Processor architecture:   'MSIL'
Public key token:         'b03f5f7f11d50a3a'
Flags:                    'PublicKey'
Full name:                'System.Runtime.CompilerServices.Unsafe, Version=4.0.6.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
```

### Example output 2

```
sver -f System.Runtime.CompilerServices.Unsafe.dll
```

```
--- File info:
File name:                'System.Runtime.CompilerServices.Unsafe.dll'
File path:                'C:\Temp\System.Runtime.CompilerServices.Unsafe.dll'
File size:                17,000 bytes
Created:                  2020-06-03T10-26-20-0138
Modified:                 2020-02-21T03-58-04-0000
Accessed:                 2020-06-03T16-11-19-8262
```

### Example output 3

```
sver -v System.Runtime.CompilerServices.Unsafe.dll
```

```
--- Version info:
File description:         'System.Runtime.CompilerServices.Unsafe'
File version:             '4.700.20.12001'
Product name:             'Microsoft® .NET Core'
Product version:          '3.1.3+8a3ffed558ddf943c1efa87d693227722d6af094'
Copyright:                '© Microsoft Corporation. All rights reserved.'
Language:                 'Language Neutral'
Original filename:        'System.Runtime.CompilerServices.Unsafe.dll'
Internal name:            'System.Runtime.CompilerServices.Unsafe.dll'
Private build:            ''
Special build:            ''
Company name:             'Microsoft Corporation'
Trademarks:               ''
Private build:            ''
Comments:                 'System.Runtime.CompilerServices.Unsafe'
```
