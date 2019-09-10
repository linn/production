import { ReportActions } from '@linn-it/linn-form-components-library';
import { assemblyFailsDetailsActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.assemblyFailsDetails.actionType,
    reportTypes.assemblyFailsDetails.uri,
    actionTypes,
    config.appRoot
);
