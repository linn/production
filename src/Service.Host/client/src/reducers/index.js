import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import manufacturingSkills from './manufacturingSkills/manufacturingSkills';
import manufacturingSkill from './manufacturingSkills/manufacturingSkill';
import manufacturingResources from './manufacturingResources/manufacturingResources';
import manufacturingResource from './manufacturingResources/manufacturingResource';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    departments,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    manufacturingSkills,
    manufacturingSkill,
    manufacturingResources,
    manufacturingResource,
    ...sharedLibraryReducers,
    buildsDetailReport
});

export default rootReducer;
