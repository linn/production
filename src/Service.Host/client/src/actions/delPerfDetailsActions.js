import { ReportActions } from '@linn-it/linn-form-components-library';
import { delPerfDetailsActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.delPerfDetails.item,
    reportTypes.delPerfDetails.actionType,
    reportTypes.delPerfDetails.uri,
    actionTypes,
    config.appRoot
);
