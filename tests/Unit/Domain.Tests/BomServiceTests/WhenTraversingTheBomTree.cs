namespace Linn.Production.Domain.Tests.BomServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Production.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenTraversingTheBomTree : ContextBase
    {
        private Bom root;

        /* nodes follow a naming convention as follows
                   __root__
                  /    \   \
                _n1_    n2  n3
               /  |  \   \    \ 
             n11 n12 n13  ...  ... 
            /   \      \
          n111   n112   ...
          /       |
        ...      n1121
         
        */
        
        private Bom n1;   // first child of root

        private Bom n2;   // ...

        private Bom n3;   // ...

        private Bom n11;  // first child of first child of root

        private Bom n12;  // second child of first child of root

        private Bom n13;  // ...

        private Bom n111; // first child of first child of first child of root 

        private Bom n112; // etc

        private Bom n1121;

        [SetUp]
        public void SetUp()
        {
           this.BuildBomTree();
           this.MockRepository.FindBy(Arg.Any<Expression<Func<Bom, bool>>>()).Returns(
               this.root, 
               this.n1,
               this.n2,
               this.n3,
               this.n11,
               this.n12,
               this.n13,
               this.n111,
               this.n112,
               this.n1121);
        }

        [Test]

        public void ShouldAddAllNodesToResultListBreadthFirst()
        {
            this.Sut.GetAllAssembliesOnBom("ROOT").Count().Should().Be(10);
        }

        private void BuildBomTree()
        {
            this.n111 = new Bom { BomName = "n111", Details = new List<BomDetail>() };

            this.n1121 = new Bom { BomName = "n1121", Details = new List<BomDetail>() };

            this.n112
                = new Bom
                {
                    BomName = "ROOT",
                    Details = new List<BomDetail>
                                                          {
                                                              new BomDetail
                                                                  {
                                                                      Bom = this.n1121,
                                                                      ChangeState = "LIVE",
                                                                      Part = new Part { BomType = "A"}
                                                                  }
                                                          }
                };

            this.n11
                = new Bom
                {
                    BomName = "n11",
                    Details = new List<BomDetail>
                                        {
                                            new BomDetail
                                              {
                                                  Bom = this.n111,
                                                  ChangeState = "LIVE",
                                                  Part = new Part { BomType = "A"}
                                              },
                                            new BomDetail
                                                {
                                                    Bom = this.n112,
                                                    ChangeState = "LIVE",
                                                    Part = new Part { BomType = "A"}
                                                }
                                        }
                };

            this.n12 = new Bom { BomName = "n12", Details = new List<BomDetail>() };

            this.n13 = new Bom { BomName = "n13", Details = new List<BomDetail>() };

            this.n1 = new Bom
            {
                BomName = "n1",
                Details = new List<BomDetail>
                                            {
                                                new BomDetail { Bom = this.n11, ChangeState = "LIVE", Part = new Part { BomType = "A" } },
                                                new BomDetail { Bom = this.n12, ChangeState = "LIVE", Part = new Part { BomType = "A" } },
                                                new BomDetail { Bom = this.n13, ChangeState = "LIVE", Part = new Part { BomType = "A" } },
                                            }
            };

            this.n2 = new Bom
            {
                BomName = "n2",
                Details = new List<BomDetail>()
            };

            this.n3 = new Bom
            {
                BomName = "n3",
                Details = new List<BomDetail>()
            };

            this.root = new Bom
            {
                BomName = "ROOT",
                Details = new List<BomDetail>
                                              {
                                                  new BomDetail { Bom = this.n1, ChangeState = "LIVE", Part = new Part { BomType = "A" } },
                                                  new BomDetail { Bom = this.n2, ChangeState = "LIVE", Part = new Part { BomType = "A" } },
                                                  new BomDetail { Bom = this.n3, ChangeState = "LIVE", Part = new Part { BomType = "A" } },
                                              }
            };
        }
    }
}
