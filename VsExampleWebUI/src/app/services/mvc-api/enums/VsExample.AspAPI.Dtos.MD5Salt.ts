/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "VsExample.AspAPI"
 * Source Type: "C:\Users\erris\Documents\GitHub\VisualStudioExamples\VsExample.AspAPI\bin\Debug\netcoreapp2.2\VsExample.AspAPI.dll"
 * Generated At: 2019-03-17 17:00:18.923
 */

export type MD5Salt = 'MD5Salt_47924720sfhosf';

declare global{
	interface Number{
		toMD5Salt (): MD5Salt;
	}
}

export class MD5SaltConverter extends Number {
	public static convert (value: number): MD5Salt {
		switch(value){
			case 0:
				return 'MD5Salt_47924720sfhosf';
		}
	}
	public static parse (value: string): number | undefined {
		switch(value){
			case 'MD5Salt_47924720sfhosf':
				return 0;
		}
		return undefined;
	}
	public static all: MD5Salt[] = ['MD5Salt_47924720sfhosf'];
}

class MD5SaltExtensions extends Number {
	public toMD5Salt (): MD5Salt {
		return MD5SaltConverter.convert(<any>this);
	}
}

Number.prototype.toMD5Salt = MD5SaltExtensions.prototype.toMD5Salt;

export module MD5Salt {
	export const MD5Salt_47924720sfhosf = 'MD5Salt_47924720sfhosf';
}
