// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace Microsoft.DotNet.ProjectJsonMigration.Rules
{
    public class MigrateRuntimeOptionsRule : IMigrationRule
    {
        private static readonly string s_runtimeOptionsFileName = "runtimeconfig.template.json";

        public void Apply(MigrationSettings migrationSettings, MigrationRuleInputs migrationRuleInputs)
        {
            var projectContext = migrationRuleInputs.DefaultProjectContext;
            var raw = projectContext.ProjectFile.RawRuntimeOptions;
            var outputRuntimeOptionsFile = Path.Combine(migrationSettings.OutputDirectory, s_runtimeOptionsFileName);

            if (!string.IsNullOrEmpty(raw))
            {
                if (File.Exists(outputRuntimeOptionsFile))
                {
                    MigrationErrorCodes.MIGRATE1015(
                        $"{outputRuntimeOptionsFile} already exists. Has migration already been run?").Throw();
                }

                File.WriteAllText(outputRuntimeOptionsFile, raw);
            }
        }   
    }
}
