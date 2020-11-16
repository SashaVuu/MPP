using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TestsGeneratorLib.CodeStructUtil
{
    public class TestClassInfo
    {
        public string Name;
        public string Code;
        public TestClassInfo(string name, string code)
        {
            Name = name;
            Code = code;
        }

    }

    public class CodeStructure
    {
        

        public CodeStructure(string content)
        {
            CreateStructure(content);
        }

        private string CreateStructure(string content) 
        {

            List<TestClassInfo > generatedClasses = new List<TestClassInfo>();

            var tree = CSharpSyntaxTree.ParseText(content);
            var syntaxRoot = tree.GetRoot();

            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            var compilation = CSharpCompilation.Create("MyCompilation",
                syntaxTrees: new[] { tree }, references: new[] { mscorlib });


            var model = compilation.GetSemanticModel(tree);

            var classDeclarations = syntaxRoot.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var clsInfo in classDeclarations)
            {
                string className = clsInfo.Identifier.ValueText;
                string clsNamespace = ((NamespaceDeclarationSyntax)clsInfo.Parent).Name.ToString();

                NamespaceDeclarationSyntax template_namespace = NamespaceDeclaration(
                    QualifiedName(
                        IdentifierName(className), IdentifierName("Test")));

                var template_usings = GetTemplateUsing(clsNamespace);

                var template_methods = GetTemplateMethods(clsInfo, model);
                var template_fields = GetTemplateFields(clsInfo, model);
                var template_members = List(template_fields.Concat(template_methods));

                var template_classname = className + "Tests";

                //Class declaration
                var classTemplate =
                  CompilationUnit()
                     .WithUsings(template_usings)
                     .WithMembers(SingletonList<MemberDeclarationSyntax>(template_namespace
                         .WithMembers(SingletonList<MemberDeclarationSyntax>(ClassDeclaration(template_classname)
                             .WithAttributeLists(
                                 SingletonList(
                                     AttributeList(
                                         SingletonSeparatedList(
                                             Attribute(
                                                 IdentifierName("TestClass"))))))
                             .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                             .WithMembers(template_members)))));

                string generatedCode = classTemplate.NormalizeWhitespace().ToFullString();
            }
                return "";
        }


        private List<NamespaceDeclarationSyntax> DefineNamespaces(SyntaxNode root) 
        {
            var kek = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>();
            foreach (NamespaceDeclarationSyntax n in kek)
            {
                DefineClasses(n);
            }
            return null;
        }

        //По неймспейсу находит все классы и добавляет их 
        private List<ClassDeclarationSyntax> DefineClasses(NamespaceDeclarationSyntax nds)
        {
            return null;
        }

        private void DefineMethods()
        {
        }

        private void DefineFields()
        {
        }

    }
}
