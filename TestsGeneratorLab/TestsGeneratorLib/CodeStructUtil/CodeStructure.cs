using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestsGeneratorLib.CodeStructUtil
{
    public class CodeStructure
    {
        Namespace[] namespaces;
        SyntaxNode root;
        public CodeStructure(string content)
        {

            root = CSharpSyntaxTree.ParseText(content).GetRoot();
            namespaces[0] = new Namespace();

        }
    }
}
