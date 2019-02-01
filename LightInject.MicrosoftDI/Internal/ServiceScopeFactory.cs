/*********************************************************************************
    The MIT License (MIT)
    Copyright (c) 2018 bernhard.richter@gmail.com
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
******************************************************************************
    LightInject.Microsoft.DependencyInjection version 2.2.0
    http://www.lightinject.net/
    http://twitter.com/bernhardrichter
******************************************************************************/
namespace LightInject.MicrosoftDI.Internal
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// An <see cref="IServiceScopeFactory"/> that uses an <see cref="IServiceContainer"/> to create new scopes.
    /// </summary>
    internal class ServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IServiceContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceScopeFactory"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IServiceContainer"/> used to create new scopes.</param>
        public ServiceScopeFactory(IServiceContainer container)
        {
            this.container = container;
        }

        /// <inheritdoc/>
        public IServiceScope CreateScope()
        {
            var scope = container.BeginScope();

            return new ServiceScope(scope);
        }
    }
}