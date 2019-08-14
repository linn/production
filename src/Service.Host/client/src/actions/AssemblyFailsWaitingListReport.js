import { ReportActions } from '@linn-it/linn-form-components-library';
import { assemblyFailsWaitingListActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.assemblyFailsWaitingList.actionType,
    reportTypes.assemblyFailsWaitingList.uri,
    actionTypes,
    config.appRoot
);
