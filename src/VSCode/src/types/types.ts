import * as vscode from 'vscode';

/**
 * Interface representing a file item in quick pick menu
 */
export interface IFileQuickPickItem extends vscode.QuickPickItem {
  path: string;
}

/**
 * Generator interface for C# generators
 */
export interface IGenerator {
  command: string;
  displayName: string;
  requiresJava: boolean;
}

/**
 * Rapicgen version status return type
 */
export interface IRapicgenVersionStatus {
  needsUpdate: boolean;
  currentVersion: string | null;
  targetVersion: string | null;
}