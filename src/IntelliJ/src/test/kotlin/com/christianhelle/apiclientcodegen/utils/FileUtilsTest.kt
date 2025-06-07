package com.christianhelle.apiclientcodegen.utils

import org.junit.Assert.*
import org.junit.Test
import org.mockito.Mockito.*
import com.intellij.openapi.vfs.VirtualFile

class FileUtilsTest {
    
    @Test
    fun testIsOpenApiSpecificationFile() {
        val jsonFile = mock(VirtualFile::class.java)
        `when`(jsonFile.extension).thenReturn("json")
        assertTrue(FileUtils.isOpenApiSpecificationFile(jsonFile))
        
        val yamlFile = mock(VirtualFile::class.java)
        `when`(yamlFile.extension).thenReturn("yaml")
        assertTrue(FileUtils.isOpenApiSpecificationFile(yamlFile))
        
        val ymlFile = mock(VirtualFile::class.java)
        `when`(ymlFile.extension).thenReturn("yml")
        assertTrue(FileUtils.isOpenApiSpecificationFile(ymlFile))
        
        val txtFile = mock(VirtualFile::class.java)
        `when`(txtFile.extension).thenReturn("txt")
        assertFalse(FileUtils.isOpenApiSpecificationFile(txtFile))
        
        val noExtFile = mock(VirtualFile::class.java)
        `when`(noExtFile.extension).thenReturn(null)
        assertFalse(FileUtils.isOpenApiSpecificationFile(noExtFile))
    }
    
    @Test
    fun testIsRefitterSettingsFile() {
        val refitterFile = mock(VirtualFile::class.java)
        `when`(refitterFile.extension).thenReturn("refitter")
        assertTrue(FileUtils.isRefitterSettingsFile(refitterFile))
        
        val jsonFile = mock(VirtualFile::class.java)
        `when`(jsonFile.extension).thenReturn("json")
        assertFalse(FileUtils.isRefitterSettingsFile(jsonFile))
        
        val noExtFile = mock(VirtualFile::class.java)
        `when`(noExtFile.extension).thenReturn(null)
        assertFalse(FileUtils.isRefitterSettingsFile(noExtFile))
    }
    
    @Test
    fun testValidateSpecificationFile() {
        val validFile = mock(VirtualFile::class.java)
        `when`(validFile.exists()).thenReturn(true)
        `when`(validFile.isDirectory).thenReturn(false)
        `when`(validFile.extension).thenReturn("json")
        assertTrue(FileUtils.validateSpecificationFile(validFile))
        
        val nonExistentFile = mock(VirtualFile::class.java)
        `when`(nonExistentFile.exists()).thenReturn(false)
        assertFalse(FileUtils.validateSpecificationFile(nonExistentFile))
        
        val directoryFile = mock(VirtualFile::class.java)
        `when`(directoryFile.exists()).thenReturn(true)
        `when`(directoryFile.isDirectory).thenReturn(true)
        assertFalse(FileUtils.validateSpecificationFile(directoryFile))
        
        val invalidExtFile = mock(VirtualFile::class.java)
        `when`(invalidExtFile.exists()).thenReturn(true)
        `when`(invalidExtFile.isDirectory).thenReturn(false)
        `when`(invalidExtFile.extension).thenReturn("txt")
        assertFalse(FileUtils.validateSpecificationFile(invalidExtFile))
    }
    
    @Test
    fun testGetOutputFilePath() {
        val result = FileUtils.getOutputFilePath("/path/to/swagger.json")
        assertTrue(result.endsWith("swagger.cs"))
        
        val resultWithCustomExt = FileUtils.getOutputFilePath("/path/to/swagger.yaml", ".ts")
        assertTrue(resultWithCustomExt.endsWith("swagger.ts"))
    }
}
