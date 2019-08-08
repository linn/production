import { reducers as sharedLibraryReducers } from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import ateFaultCode from './ateFaultCode';
import ateFaultCodes from './ateFaultCodes';
import departments from './departments';
import buildsSummaryReport from './buildsSummaryReport';
import buildsDetailReport from './buildsDetailReport';
import outstandingWorksOrdersReport from './outstandingWorksOrdersReport';
import productionMeasures from './productionMeasures';
import manufacturingSkills from './manufacturingSkills/manufacturingSkills';
import manufacturingSkill from './manufacturingSkills/manufacturingSkill';

const rootReducer = combineReducers({
    oidc,
    ateFaultCode,
    ateFaultCodes,
    departments,
    buildsSummaryReport,
    outstandingWorksOrdersReport,
    productionMeasures,
    buildsDetailReport,
    manufacturingSkills,
    manufacturingSkill,
    ...sharedLibraryReducers
});

export default rootReducer;
