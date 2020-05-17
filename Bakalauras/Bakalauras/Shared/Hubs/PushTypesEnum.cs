using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Shared.Hubs
{
    public enum PushTypesEnum
    {
        visualizationCreated = 1,
        visualizationUpdated = 2,
        subjectCreated = 3,
        subjectUpdated = 4,
        lessonCreated = 5,
        lessonUpdated = 6,
        accountCreated = 7,
        accountUpdated = 8
    }
}
