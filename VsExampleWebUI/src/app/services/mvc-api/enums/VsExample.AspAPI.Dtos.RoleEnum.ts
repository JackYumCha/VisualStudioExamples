/** 
 * Auto Generated Code
 * Please do not modify this file manually 
 * Assembly Name: "VsExample.AspAPI"
 * Source Type: "C:\Users\erris\Documents\GitHub\VisualStudioExamples\VsExample.AspAPI\bin\Debug\netcoreapp2.2\VsExample.AspAPI.dll"
 * Generated At: 2019-03-24 15:47:32.526
 */

export type RoleEnum = 'Administrator'|'User'|'Visitor';

declare global{
	interface Number{
		toRoleEnum (): RoleEnum;
	}
}

export class RoleEnumConverter extends Number {
	public static convert (value: number): RoleEnum {
		switch(value){
			case 0:
				return 'Administrator';
			case 1:
				return 'User';
			case 2:
				return 'Visitor';
		}
	}
	public static parse (value: string): number | undefined {
		switch(value){
			case 'Administrator':
				return 0;
			case 'User':
				return 1;
			case 'Visitor':
				return 2;
		}
		return undefined;
	}
	public static all: RoleEnum[] = ['Administrator', 'User', 'Visitor'];
}

class RoleEnumExtensions extends Number {
	public toRoleEnum (): RoleEnum {
		return RoleEnumConverter.convert(<any>this);
	}
}

Number.prototype.toRoleEnum = RoleEnumExtensions.prototype.toRoleEnum;

export module RoleEnum {
	export const Administrator = 'Administrator';
	export const User = 'User';
	export const Visitor = 'Visitor';
}
