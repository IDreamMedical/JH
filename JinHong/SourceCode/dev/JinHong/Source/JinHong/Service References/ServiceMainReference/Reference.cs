﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JinHong.ServiceMainReference {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceMainReference.ServiceMainSoap")]
    public interface ServiceMainSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Select", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Select(string sql, string parameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Scalar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object Scalar(string sql, string parameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/NonQuery", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int NonQuery(string sql, string parameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetIds", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetIds(string typeName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetColumnData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetColumnData(string typeName, string columnName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Exists", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool Exists(string typeName, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Load", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Load(string typeName, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Delete", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool Delete(string typeName, string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Insert", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool Insert(string typeName, string strObj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Update", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool Update(string typeName, string strObj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Save", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool Save(string typeName, string strObj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ExistsFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool ExistsFile(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ExistsDirectory", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool ExistsDirectory(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeleteDirectory", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void DeleteDirectory(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DeleteFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void DeleteFile(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RenameDirectory", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void RenameDirectory(string path, string newPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RenameFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void RenameFile(string path, string newPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetFile(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetDirectory", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetDirectory(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetDirectories", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetDirectories(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetFiles", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetFiles(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetFilessWithPattern", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetFilessWithPattern(string path, string searchPattern);
        
        // CODEGEN: Parameter 'DownloadFileResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DownloadFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        JinHong.ServiceMainReference.DownloadFileResponse DownloadFile(JinHong.ServiceMainReference.DownloadFileRequest request);
        
        // CODEGEN: Parameter 'bsFile' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UploadFile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        JinHong.ServiceMainReference.UploadFileResponse UploadFile(JinHong.ServiceMainReference.UploadFileRequest request);
        
        // CODEGEN: Parameter 'GetFileMD5Result' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetFileMD5", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        JinHong.ServiceMainReference.GetFileMD5Response GetFileMD5(JinHong.ServiceMainReference.GetFileMD5Request request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownloadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string path;
        
        public DownloadFileRequest() {
        }
        
        public DownloadFileRequest(string path) {
            this.path = path;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownloadFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] DownloadFileResult;
        
        public DownloadFileResponse() {
        }
        
        public DownloadFileResponse(byte[] DownloadFileResult) {
            this.DownloadFileResult = DownloadFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string path;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] bsFile;
        
        public UploadFileRequest() {
        }
        
        public UploadFileRequest(string path, byte[] bsFile) {
            this.path = path;
            this.bsFile = bsFile;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileResponse {
        
        public UploadFileResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetFileMD5", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetFileMD5Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string path;
        
        public GetFileMD5Request() {
        }
        
        public GetFileMD5Request(string path) {
            this.path = path;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetFileMD5Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetFileMD5Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetFileMD5Result;
        
        public GetFileMD5Response() {
        }
        
        public GetFileMD5Response(byte[] GetFileMD5Result) {
            this.GetFileMD5Result = GetFileMD5Result;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceMainSoapChannel : JinHong.ServiceMainReference.ServiceMainSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceMainSoapClient : System.ServiceModel.ClientBase<JinHong.ServiceMainReference.ServiceMainSoap>, JinHong.ServiceMainReference.ServiceMainSoap {
        
        public ServiceMainSoapClient() {
        }
        
        public ServiceMainSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceMainSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceMainSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceMainSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public System.Data.DataSet Select(string sql, string parameters) {
            return base.Channel.Select(sql, parameters);
        }
        
        public object Scalar(string sql, string parameters) {
            return base.Channel.Scalar(sql, parameters);
        }
        
        public int NonQuery(string sql, string parameters) {
            return base.Channel.NonQuery(sql, parameters);
        }
        
        public string[] GetIds(string typeName) {
            return base.Channel.GetIds(typeName);
        }
        
        public string[] GetColumnData(string typeName, string columnName) {
            return base.Channel.GetColumnData(typeName, columnName);
        }
        
        public bool Exists(string typeName, string id) {
            return base.Channel.Exists(typeName, id);
        }
        
        public string Load(string typeName, string id) {
            return base.Channel.Load(typeName, id);
        }
        
        public bool Delete(string typeName, string id) {
            return base.Channel.Delete(typeName, id);
        }
        
        public bool Insert(string typeName, string strObj) {
            return base.Channel.Insert(typeName, strObj);
        }
        
        public bool Update(string typeName, string strObj) {
            return base.Channel.Update(typeName, strObj);
        }
        
        public bool Save(string typeName, string strObj) {
            return base.Channel.Save(typeName, strObj);
        }
        
        public bool ExistsFile(string path) {
            return base.Channel.ExistsFile(path);
        }
        
        public bool ExistsDirectory(string path) {
            return base.Channel.ExistsDirectory(path);
        }
        
        public void DeleteDirectory(string path) {
            base.Channel.DeleteDirectory(path);
        }
        
        public void DeleteFile(string path) {
            base.Channel.DeleteFile(path);
        }
        
        public void RenameDirectory(string path, string newPath) {
            base.Channel.RenameDirectory(path, newPath);
        }
        
        public void RenameFile(string path, string newPath) {
            base.Channel.RenameFile(path, newPath);
        }
        
        public string GetFile(string path) {
            return base.Channel.GetFile(path);
        }
        
        public string GetDirectory(string path) {
            return base.Channel.GetDirectory(path);
        }
        
        public string[] GetDirectories(string path) {
            return base.Channel.GetDirectories(path);
        }
        
        public string[] GetFiles(string path) {
            return base.Channel.GetFiles(path);
        }
        
        public string[] GetFilessWithPattern(string path, string searchPattern) {
            return base.Channel.GetFilessWithPattern(path, searchPattern);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        JinHong.ServiceMainReference.DownloadFileResponse JinHong.ServiceMainReference.ServiceMainSoap.DownloadFile(JinHong.ServiceMainReference.DownloadFileRequest request) {
            return base.Channel.DownloadFile(request);
        }
        
        public byte[] DownloadFile(string path) {
            JinHong.ServiceMainReference.DownloadFileRequest inValue = new JinHong.ServiceMainReference.DownloadFileRequest();
            inValue.path = path;
            JinHong.ServiceMainReference.DownloadFileResponse retVal = ((JinHong.ServiceMainReference.ServiceMainSoap)(this)).DownloadFile(inValue);
            return retVal.DownloadFileResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        JinHong.ServiceMainReference.UploadFileResponse JinHong.ServiceMainReference.ServiceMainSoap.UploadFile(JinHong.ServiceMainReference.UploadFileRequest request) {
            return base.Channel.UploadFile(request);
        }
        
        public void UploadFile(string path, byte[] bsFile) {
            JinHong.ServiceMainReference.UploadFileRequest inValue = new JinHong.ServiceMainReference.UploadFileRequest();
            inValue.path = path;
            inValue.bsFile = bsFile;
            JinHong.ServiceMainReference.UploadFileResponse retVal = ((JinHong.ServiceMainReference.ServiceMainSoap)(this)).UploadFile(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        JinHong.ServiceMainReference.GetFileMD5Response JinHong.ServiceMainReference.ServiceMainSoap.GetFileMD5(JinHong.ServiceMainReference.GetFileMD5Request request) {
            return base.Channel.GetFileMD5(request);
        }
        
        public byte[] GetFileMD5(string path) {
            JinHong.ServiceMainReference.GetFileMD5Request inValue = new JinHong.ServiceMainReference.GetFileMD5Request();
            inValue.path = path;
            JinHong.ServiceMainReference.GetFileMD5Response retVal = ((JinHong.ServiceMainReference.ServiceMainSoap)(this)).GetFileMD5(inValue);
            return retVal.GetFileMD5Result;
        }
    }
}
