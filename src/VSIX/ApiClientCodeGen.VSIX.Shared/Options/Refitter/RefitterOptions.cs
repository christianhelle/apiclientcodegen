﻿using System;
using System.Diagnostics.CodeAnalysis;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.Refitter;

namespace Rapicgen.Options.Refitter
{
    [ExcludeFromCodeCoverage]
    public class RefitterOptions
        : OptionsBase<IRefitterOptions, RefitterOptionsPage>, IRefitterOptions
    {
        public RefitterOptions(IRefitterOptions? options = null)
        {
            try
            {
                options ??= GetFromDialogPage();

                GenerateContracts = options.GenerateContracts;
                GenerateXmlDocCodeComments = options.GenerateXmlDocCodeComments;
                AddAutoGeneratedHeader = options.AddAutoGeneratedHeader;
                ReturnIApiResponse = options.ReturnIApiResponse;
                GenerateInternalTypes = options.GenerateInternalTypes;
                UseCancellationTokens = options.UseCancellationTokens;
                GenerateHeaderParameters = options.GenerateHeaderParameters;
                GenerateMultipleFiles = options.GenerateMultipleFiles;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);

                GenerateContracts = true;
                GenerateXmlDocCodeComments = true;
                GenerateHeaderParameters = true;
            }
        }

        public bool GenerateContracts { get; set; }
        public bool GenerateXmlDocCodeComments { get; set; }
        public bool AddAutoGeneratedHeader { get; set; }
        public bool ReturnIApiResponse { get; set; }
        public bool GenerateInternalTypes { get; set; }
        public bool UseCancellationTokens { get; set; }
        public bool GenerateHeaderParameters { get; set; }
        public bool GenerateMultipleFiles { get; set; }
    }
}