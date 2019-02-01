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
    using System;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// An <see cref="IServiceScope"/> implementation that wraps a <see cref="scope"/>.
    /// </summary>
    internal class ServiceScope : IServiceScope
    {
        private Scope scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceScope"/> class.
        /// </summary>
        /// <param name="scope">The <see cref="scope"/> wrapped by this class.</param>
        public ServiceScope(Scope scope)
        {
            this.scope = scope;
            this.ServiceProvider = new ServiceProvider(scope);
        }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}