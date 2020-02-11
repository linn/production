import { ProcessActions } from '@linn-it/linn-form-components-library';
import { issueSernosActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.issueSernos.item,
    processTypes.issueSernos.actionType,
    processTypes.issueSernos.uri,
    actionTypes,
    config.appRoot
);
