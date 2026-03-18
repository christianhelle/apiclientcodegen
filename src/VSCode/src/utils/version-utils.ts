import * as vscode from 'vscode';
import * as path from 'path';
import * as fs from 'fs';

/**
 * Gets the extension version from package.json
 * @param context The extension context
 * @returns The version string or undefined if not found
 */
export function getExtensionVersion(context: vscode.ExtensionContext): string | undefined {
  try {
    // Get the extension's package.json path
    const packageJsonPath = path.join(context.extensionPath, 'package.json');
    if (fs.existsSync(packageJsonPath)) {
      const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
      return packageJson.version;
    }
  } catch (error) {
    console.error('Error reading extension version:', error);
  }
  return undefined;
}

/**
 * Compares two version strings
 * @param version1 First version string
 * @param version2 Second version string
 * @returns 1 if version1 > version2, -1 if version1 < version2, 0 if equal
 */
export function compareVersions(version1: string, version2: string): number {
  const parts1 = version1.split('.').map(s => {
    const num = parseInt(s, 10);
    return isNaN(num) ? 0 : num;
  });
  const parts2 = version2.split('.').map(s => {
    const num = parseInt(s, 10);
    return isNaN(num) ? 0 : num;
  });
  
  for (let i = 0; i < Math.max(parts1.length, parts2.length); i++) {
    const p1 = i < parts1.length ? parts1[i] : 0;
    const p2 = i < parts2.length ? parts2[i] : 0;
    
    if (p1 > p2) {
      return 1;
    }
    if (p1 < p2) {
      return -1;
    }
  }
  
  return 0;
}