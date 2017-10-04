// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    ///     Factory for creating robots.txt files.
    /// </summary>
    public interface IRobotsTxtFactory
    {
        /// <summary>
        ///     Creates the robots.txt file and returns it as a string.
        /// </summary>
        /// <returns>The robots.txt file.</returns>
        string Create();
    }
}